
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static string? ForbidCookie (
    HttpContext context,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions,
    ILogger logger)
  {
    var returnUri = GetAuthenticationPropertiesRedirectUri(authProperties) ?? BuildRelativeUri(context.Request);
    var forbidPath = BuildForbidPath(authOptions, returnUri);

    LogForbidden(logger, authOptions.SchemeName, forbidPath, context.TraceIdentifier);
    return SetResponseRedirect(context.Response, forbidPath)!;
  }

  public static string? ForbidCookie (
    HttpContext context,
    AuthenticationProperties authProperties) =>
      ForbidCookie(
        context,
        authProperties,
        ResolveRequiredService<CookieAuthenticationOptions>(context),
        ResolveCookiesLogger(context));
}