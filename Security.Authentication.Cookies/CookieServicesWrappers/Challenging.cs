
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static string ChallengeCookie(HttpContext context, AuthenticationProperties authProperties) =>
    ChallengeCookie(
      context,
      authProperties,
      ResolveRequiredService<AuthenticationCookieOptions>(context),
      ResolveCookiesLogger(context));
}