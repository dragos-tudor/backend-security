
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static string? ChallengeCookie (
    HttpContext context,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions)
  {
    var returnUri = GetAuthenticationPropertiesRedirectUri(authProperties) ?? BuildRelativeUri(context.Request);
    var challengePath = BuildChallengePath(authOptions, returnUri);

    LogChallenged(ResolveCookiesLogger(context), authOptions.SchemeName, challengePath, context.TraceIdentifier);
    return SetResponseRedirect(context.Response, challengePath)!;
  }

  public static string? ChallengeCookie (
    HttpContext context,
    AuthenticationProperties authProperties) =>
      ChallengeCookie(
        context,
        authProperties,
        ResolveService<CookieAuthenticationOptions>(context));
}