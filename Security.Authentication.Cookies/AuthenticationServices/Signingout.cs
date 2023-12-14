
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  public static string? SignOutCookie (
    HttpContext context,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder,
    ICookieManager cookieManager)
  {
    var cookieOptions = BuildCookieOptions(cookieBuilder, context);
    var cookieName = GetCookieName(cookieBuilder, authOptions);
    UnsetResponseCookieHeader(context, cookieManager, cookieOptions, cookieName);
    ResetResponseCacheHeaders(context.Response);

    if (IsRequestLogoutPath(context.Request, authOptions))
    if (GetPropertiesRedirectUriOrQueryReturnUrl(context, authProperties, authOptions.ReturnUrlParameter) is string redirectUrl)
      SetResponseRedirect(context.Response, redirectUrl);

    LogSignedOutCookie(Logger, authOptions.SchemeName, context.TraceIdentifier);
    return GetResponseLocation(context.Response);
  }

  public static string? SignOutCookie (
    HttpContext context,
    AuthenticationProperties? authProperties = default) =>
      SignOutCookie(
        context,
        authProperties ?? CreateAuthenticationProperties(),
        ResolveService<CookieAuthenticationOptions>(context),
        ResolveService<CookieBuilder>(context),
        ResolveService<ICookieManager>(context));

}