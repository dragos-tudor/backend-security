
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  internal const string MissingSessionTicketId = "Missing session ticket id";
  internal const string MissingSessionTicket = "Missing session ticket";

  internal static async Task<AuthenticateResult> AuthenticateSessionCookie(
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    ICookieManager cookieManager,
    TicketDataFormat ticketDataFormat,
    ITicketStore ticketStore,
    DateTimeOffset currentUtc,
    string? ticketId)
  {
    if (ticketId is null) return Fail(MissingSessionTicketId);

    var sessionTicket = await GetSessionTicket(ticketStore, ticketId, context.RequestAborted);
    if (sessionTicket is null) return Fail(MissingSessionTicket);

    var authResult = GetAuthenticationTicketState(sessionTicket, currentUtc, authOptions) switch {
      AuthenticationTicketState.Expired => Fail(TicketExpired),
      AuthenticationTicketState.Renewable => Success(RenewAuthenticationTicket(sessionTicket, currentUtc)),
      _ => Success(sessionTicket)
    };

    if (IsExpiredAuthenticationTicket(authResult)) await RemoveSessionTicket(ticketStore, ticketId, context.RequestAborted);
    if (IsRenewedAuthenticationTicket(authResult, currentUtc)) await RenewSessionTicket(ticketStore, authResult.Ticket!, ticketId, context.RequestAborted);

    var cookieName = GetCookieName(authOptions);
    var cookieOptions = BuildCookieOptions(sessionTicket.Properties!, context);

    if (IsExpiredAuthenticationTicket(authResult)) DeleteCookie(context, cookieManager, cookieName, cookieOptions);
    if (IsRenewedAuthenticationTicket(authResult, currentUtc)) AppendCookie(context, cookieManager, cookieName,
      ProtectAuthenticationTicket(CreateSessionIdTicket(ticketId, authOptions.SchemeName), ticketDataFormat), cookieOptions);

    return authResult;
  }

  internal static Task<AuthenticateResult> AuthenticateSessionCookie(
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    string? ticketId) =>
      AuthenticateSessionCookie(
        context,
        authOptions,
        ResolveRequiredService<ICookieManager>(context),
        ResolveRequiredService<TicketDataFormat>(context),
        ResolveRequiredService<ITicketStore>(context),
        ResolveRequiredService<TimeProvider>(context).GetUtcNow(),
        ticketId
      );

}