
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class Funcs {

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

}