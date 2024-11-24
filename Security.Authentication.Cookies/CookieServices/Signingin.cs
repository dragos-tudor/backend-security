
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static async ValueTask<AuthenticationTicket> SignInCookie(
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
    if(IsSessionBasedTicket(ticketStore))
      return await SignInSessionCookie(context, principal, authProps, authOptions, currentUtc, cookieManager, authTicketProtector, ticketStore, logger);

    var authTicket = CreateAuthenticationTicket(principal, authProps, authOptions.SchemeName);
    SetAuthPropsExpiration(authProps, currentUtc, authOptions.ExpireAfter);
    SetAuthenticationCookie(context, authTicket, authOptions, currentUtc, cookieManager, authTicketProtector);
    ResetHttpResponseCacheHeaders(context.Response);

    LogSignedInCookie(logger, authOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
    return authTicket;
  }
}