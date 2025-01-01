
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static async Task<AuthenticationTicket> SignInSessionCookie(
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties authProps,
    AuthenticationCookieOptions authOptions,
    DateTimeOffset currentUtc,
    ICookieManager cookieManager,
    TicketDataFormat authTicketProtector,
    ITicketStore ticketStore,
    ILogger logger)
  {
    var sessionTicket = CreateAuthenticationTicket(principal, authProps, authOptions.SchemeName);
    SetAuthPropsExpiration(authProps, currentUtc, authOptions.ExpireAfter);

    var sessionId = await SetSessionTicket(ticketStore, sessionTicket, context.RequestAborted);
    var sessionTicketId = CreateSessionTicketId(sessionId, authOptions.SchemeName);
    SetAuthPropsExpiration(sessionTicketId.Properties, currentUtc, authOptions.ExpireAfter);
    SetAuthenticationCookie(context, sessionTicketId, authOptions, currentUtc, cookieManager, authTicketProtector);
    ResetHttpResponseCacheHeaders(context.Response);

    LogSignedInCookie(logger, authOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
    return sessionTicket;
  }
}