
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class Funcs {

  public static AuthenticationTicket SignInCookie (
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder,
    DateTimeOffset currentUtc)
  {
    SetAuthenticationPropertiesIssued(authProperties, currentUtc);
    SetAuthenticationPropertiesExpires(authProperties, authOptions.ExpireTimeSpan, currentUtc);

    var cookieOptions = BuildCookieOptions(cookieBuilder, context);
    var cookieName = GetCookieName(cookieBuilder, authOptions);
    SetCookieOptionsExpires(cookieOptions, IsAuthenticationPropertiesPersistent(authProperties) ? GetAuthenticationPropertiesExpires(authProperties) : default);
    SetCookieOptionsSecure(cookieOptions, IsSecuredCookie(context, cookieBuilder.SecurePolicy));

    var ticket = CreateAuthenticationTicket(principal, authProperties, authOptions.SchemeName);
    var protectedTicket = ProtectAuthenticationTicket(ticket, authOptions.TicketDataFormat);
    SetResponseCookieHeader(context, protectedTicket, authOptions.CookieManager, cookieOptions, cookieName);
    ResetResponseCacheHeaders(context.Response);
    if (IsRequestLoginPath(context.Request, authOptions))
    if (GetPropertiesRedirectUriOrQueryReturnUrl(context, authProperties, authOptions.ReturnUrlParameter) is string redirectUrl)
      SetResponseRedirect(context.Response, redirectUrl);

    LogSignedInCookie(Logger, authOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
    return ticket;
  }

}