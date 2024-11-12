
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static async Task<AuthenticationTicket> SignInSessionCookie(
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties authProperties,
    AuthenticationCookieOptions authOptions,
    DateTimeOffset currentUtc,
    ICookieManager cookieManager,
    TicketDataFormat ticketDataProtector,
    ITicketStore ticketStore,
    ILogger logger)
  {
    SetAuthenticationPropertiesExpiration(authProperties, currentUtc, authOptions.ExpireTimeSpan);
    var sessionTicket = CreateAuthenticationTicket(principal, authProperties, authOptions.SchemeName);
    var sessionId = await SetSessionTicket(ticketStore, sessionTicket, context.RequestAborted);

    var sessionTicketId = CreateSessionTicketId(sessionId, authOptions.SchemeName);
    UseAuthenticationTicket(context, sessionTicketId, authOptions, cookieManager, ticketDataProtector);
    ResetResponseCacheHeaders(context.Response);

    LogSignedInCookie(logger, authOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
    return sessionTicketId;
  }
}