
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class Funcs {

  public static string? SignOutCookie (
    HttpContext context,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder)
  {
    var cookieOptions = BuildCookieOptions(cookieBuilder, context);
    var cookieName = GetCookieName(cookieBuilder, authOptions);
    UnsetResponseCookieHeader(context, authOptions, cookieOptions, cookieName);
    ResetResponseCacheHeaders(context.Response);

    if (IsRequestLogoutPath(context.Request, authOptions))
    if (GetPropertiesRedirectUriOrQueryReturnUrl(context, authProperties, authOptions.ReturnUrlParameter) is string redirectUrl)
      SetResponseRedirect(context.Response, redirectUrl);

    LogSignedOutCookie(Logger, authOptions.SchemeName, context.TraceIdentifier);
    return GetResponseLocation(context.Response);
  }

}