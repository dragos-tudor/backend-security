
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static void ChallengeCookie(HttpContext context) =>
    ChallengeCookie(
      context,
      ResolveRequiredService<AuthenticationCookieOptions>(context),
      ResolveCookiesLogger(context));
}