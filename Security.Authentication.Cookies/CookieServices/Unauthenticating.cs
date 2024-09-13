using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static int UnauthenticateCookie (HttpContext context) =>
    UnauthenticateAuth (context, ResolveRequiredService<CookieAuthenticationOptions>(context), ResolveCookiesLogger(context));
}