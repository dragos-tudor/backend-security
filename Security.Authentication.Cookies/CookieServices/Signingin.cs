
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static async ValueTask<AuthenticationTicket> SignInCookie(
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
    if(IsSessionBasedTicket(ticketStore))
      return await SignInSessionCookie(context, principal, authProperties, authOptions, currentUtc, cookieManager, ticketDataProtector, ticketStore, logger);

    var authTicket = CreateAuthenticationTicket(principal, authProperties, authOptions.SchemeName);
    SetAuthenticationPropertiesExpiration(authProperties, currentUtc, authOptions.ExpireTimeSpan);
    UseAuthenticationTicket(context, authTicket, authOptions, cookieManager, ticketDataProtector);
    ResetResponseCacheHeaders(context.Response);

    LogSignedInCookie(logger, authOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
    return authTicket;
  }
}