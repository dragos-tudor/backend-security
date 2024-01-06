
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  const string MissingSessionTicketId = "Missing session ticket id";
  const string MissingSessionTicket = "Missing session ticket";

  internal static async Task<AuthenticateResult> AuthenticateSessionCookie(
    HttpContext context,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder,
    ICookieManager cookieManager,
    ISecureDataFormat<AuthenticationTicket> ticketProtector,
    ITicketStore ticketStore,
    string? ticketId,
    DateTimeOffset currentUtc)
  {
    if (ticketId is null) return Fail(MissingSessionTicketId);

    var sessionTicket = await GetSessionTicket(ticketStore, ticketId, context.RequestAborted);
    if (sessionTicket is null) return Fail(MissingSessionTicket);

    var sessionCookieOptions = BuildCookieOptions(cookieBuilder, sessionTicket.Properties!, context);
    var sessionResult = GetAuthenticationTicketState(sessionTicket, currentUtc, authOptions) switch {
      AuthenticationTicketState.ExpiredTicket => Fail(TicketExpired),
      AuthenticationTicketState.RenewableTicket => Success(RenewAuthenticationTicket(sessionTicket, currentUtc)),
      _ => Success(sessionTicket)
    };

    if (IsExpiredAuthenticationTicket(sessionResult)) await RemoveSessionTicket(ticketStore, ticketId, context.RequestAborted);
    if (IsRenewableAuthenticationTicket(sessionResult, currentUtc)) await RenewSessionTicket(ticketStore, sessionResult.Ticket!, ticketId, context.RequestAborted);

    var cookieName = GetCookieName(cookieBuilder, authOptions);
    if (IsExpiredAuthenticationTicket(sessionResult)) DeleteAuthenticationCookie(context, cookieManager, cookieName, sessionCookieOptions);
    if (IsRenewableAuthenticationTicket(sessionResult, currentUtc)) AppendAuthenticationCookie(context, cookieManager, cookieName,
      ProtectAuthenticationTicket(CreateSessionIdTicket(ticketId, authOptions.SchemeName), ticketProtector), sessionCookieOptions);

    return sessionResult;
  }

}