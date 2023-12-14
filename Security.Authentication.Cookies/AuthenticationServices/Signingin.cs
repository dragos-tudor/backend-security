
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  public static AuthenticationTicket SignInCookie (
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder,
    ICookieManager cookieManager,
    ISecureDataFormat<AuthenticationTicket> secureDataFormat,
    DateTimeOffset currentUtc)
  {
    SetAuthenticationPropertiesIssued(authProperties, currentUtc);
    SetAuthenticationPropertiesExpires(authProperties, currentUtc, authOptions.ExpireTimeSpan);

    var cookieOptions = BuildCookieOptions(cookieBuilder, context);
    var cookieName = GetCookieName(cookieBuilder, authOptions);
    SetCookieOptionsExpires(cookieOptions, IsAuthenticationPropertiesPersistent(authProperties) ? GetAuthenticationPropertiesExpires(authProperties) : default);
    SetCookieOptionsSecure(cookieOptions, IsSecuredCookie(context, cookieBuilder.SecurePolicy));

    var ticket = CreateAuthenticationTicket(principal, authProperties, authOptions.SchemeName);
    var protectedTicket = ProtectAuthenticationTicket(ticket, secureDataFormat);
    SetResponseCookieHeader(context, protectedTicket, cookieManager, cookieOptions, cookieName);
    ResetResponseCacheHeaders(context.Response);
    if (IsRequestLoginPath(context.Request, authOptions))
    if (GetPropertiesRedirectUriOrQueryReturnUrl(context, authProperties, authOptions.ReturnUrlParameter) is string redirectUrl)
      SetResponseRedirect(context.Response, redirectUrl);

    LogSignedInCookie(Logger, authOptions.SchemeName, GetPrincipalNameId(principal)!, context.TraceIdentifier);
    return ticket;
  }

  public static AuthenticationTicket SignInCookie (
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties? authProperties = default) =>
      SignInCookie(
        context,
        principal,
        authProperties ?? CreateAuthenticationProperties(),
        ResolveService<CookieAuthenticationOptions>(context),
        ResolveService<CookieBuilder>(context),
        ResolveService<ICookieManager>(context),
        ResolveService<ISecureDataFormat<AuthenticationTicket>>(context),
        ResolveService<TimeProvider>(context).GetUtcNow());

}