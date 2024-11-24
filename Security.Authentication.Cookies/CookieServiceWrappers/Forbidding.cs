
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static void ForbidCookie(HttpContext context) =>
    ForbidCookie(
      context,
      ResolveRequiredService<AuthenticationCookieOptions>(context),
      ResolveCookiesLogger(context));
}