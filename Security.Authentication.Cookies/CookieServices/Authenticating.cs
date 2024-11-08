
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;
#nullable disable

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  internal const string TicketExpired = "Ticket expired";
  internal const string UnprotectTicketFailed = "Unprotect ticket failed";

  public static async ValueTask<AuthenticateResult> AuthenticateCookie (
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    ICookieManager cookieManager,
    TicketDataFormat ticketDataFormat,
    ITicketStore ticketStore,
    DateTimeOffset currentUtc)
  {
    var cookieName = GetCookieName(authOptions);
    var cookie = GetCookie(context, cookieManager, cookieName);
    if (!ExistsCookie(cookie)) return NoResult();

    var cookieTicket = UnprotectAuthenticationTicket(cookie, ticketDataFormat);
    if (!ExistsAuthenticationTicket(cookieTicket)) return Fail(UnprotectTicketFailed);

    if (IsSessionBasedCookie(ticketStore))
      return await AuthenticateSessionCookie(context, authOptions, cookieManager,
        ticketDataFormat, ticketStore, currentUtc, GetSessionTicketId(cookieTicket.Principal));

    var cookieOptions = BuildCookieOptions(cookieTicket.Properties!, context);
    var authResult = GetAuthenticationTicketState(cookieTicket, currentUtc, authOptions) switch {
      AuthenticationTicketState.Expired => Fail(TicketExpired),
      AuthenticationTicketState.Renewable => Success(RenewAuthenticationTicket(cookieTicket, currentUtc)),
      _ => Success(cookieTicket)
    };

    if (IsExpiredAuthenticationTicket(authResult)) DeleteCookie(context, cookieManager, cookieName, cookieOptions);
    if (IsRenewedAuthenticationTicket(authResult, currentUtc)) AppendCookie(context, cookieManager, cookieName,
      ProtectAuthenticationTicket(cookieTicket, ticketDataFormat), cookieOptions);

    return authResult;
  }

  public static async Task<AuthenticateResult> AuthenticateCookie (HttpContext context)
  {
    var authOptions = ResolveRequiredService<AuthenticationCookieOptions>(context);
    var authResult = await AuthenticateCookie(
      context,
      authOptions,
      ResolveRequiredService<ICookieManager>(context),
      ResolveRequiredService<TicketDataFormat>(context),
      ResolveRequiredService<ITicketStore>(context),
      ResolveRequiredService<TimeProvider>(context).GetUtcNow()
    );

    LogAuthenticationResult(ResolveCookiesLogger(context), authResult, authOptions.SchemeName, context.TraceIdentifier);
    return authResult;
  }
}