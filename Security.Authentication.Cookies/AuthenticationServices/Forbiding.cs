
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  public static string ForbidCookie (
    HttpContext context,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions)
  {
    var returnUrl = GetPropertiesRedirectUriOrCurrentUri(context, authProperties);
    var forbidPath = BuildForbidPath(authOptions, returnUrl);

    LogForbidden(Logger, authOptions.SchemeName, forbidPath, context.TraceIdentifier);
    return SetResponseRedirect(context.Response, forbidPath);
  }

  public static string ForbidCookie (
    HttpContext context,
    AuthenticationProperties? authProperties = default) =>
      ForbidCookie(
        context,
        authProperties ?? CreateAuthenticationProperties(),
        ResolveService<CookieAuthenticationOptions>(context));

}