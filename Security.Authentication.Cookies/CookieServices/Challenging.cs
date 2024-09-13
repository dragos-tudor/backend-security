
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static string? ChallengeCookie (
    HttpContext context,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions,
    ILogger logger)
  {
    var returnUri = GetAuthenticationPropertiesRedirectUri(authProperties) ?? BuildRelativeUri(context.Request);
    var challengePath = BuildChallengePath(authOptions, returnUri);

    LogChallenged(logger, authOptions.SchemeName, challengePath, context.TraceIdentifier);
    return SetResponseRedirect(context.Response, challengePath)!;
  }

  public static string? ChallengeCookie (
    HttpContext context,
    AuthenticationProperties authProperties) =>
      ChallengeCookie(
        context,
        authProperties,
        ResolveRequiredService<CookieAuthenticationOptions>(context),
        ResolveCookiesLogger(context));
}