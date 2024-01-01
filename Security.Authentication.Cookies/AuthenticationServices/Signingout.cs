
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static string? SignOutCookie (
    HttpContext context,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder,
    ICookieManager cookieManager)
  {
    var cookieOptions = BuildCookieOptions(cookieBuilder, context);
    var cookieName = GetCookieName(cookieBuilder, authOptions);
    DeleteAuthenticationCookie(context, cookieManager, cookieName, cookieOptions);
    ResetResponseCacheHeaders(context.Response);

    if (IsRequestLogoutPath(context.Request, authOptions))
    if (GetSigningRedirectUri(context, authProperties, authOptions.ReturnUrlParameter) is string redirectUri)
      SetResponseRedirect(context.Response, redirectUri);

    LogSignedOutCookie(Logger, authOptions.SchemeName, context.TraceIdentifier);
    return GetResponseLocation(context.Response);
  }

  public static string? SignOutCookie (
    HttpContext context,
    AuthenticationProperties authProperties) =>
      SignOutCookie(
        context,
        authProperties,
        ResolveService<CookieAuthenticationOptions>(context),
        ResolveService<CookieBuilder>(context),
        ResolveService<ICookieManager>(context));
}