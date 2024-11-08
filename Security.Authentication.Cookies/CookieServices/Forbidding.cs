
using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static string ForbidCookie (
    HttpContext context,
    AuthenticationProperties authProperties,
    AuthenticationCookieOptions authOptions,
    ILogger logger)
  {
    SetResponseStatus(context, HttpStatusCode.Forbidden);
    var returnUri = GetAuthenticationPropertiesRedirectUri(authProperties!) ?? BuildRelativeUri(context.Request);
    var forbidPath = BuildForbidPath(authOptions, returnUri);

    LogForbidden(logger, authOptions.SchemeName, forbidPath, context.TraceIdentifier);
    return forbidPath;
  }

  public static string ForbidCookie (HttpContext context, AuthenticationProperties authProperties) =>
    ForbidCookie (
      context,
      authProperties,
      ResolveRequiredService<AuthenticationCookieOptions>(context),
      ResolveCookiesLogger(context));
}