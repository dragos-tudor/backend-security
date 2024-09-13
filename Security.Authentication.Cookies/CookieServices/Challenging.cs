
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static string? ChallengeCookie (HttpContext context, AuthenticationProperties? authProperties = default) =>
    ChallengeAuth (context, ResolveRequiredService<CookieAuthenticationOptions>(context), ResolveCookiesLogger(context), authProperties);
}