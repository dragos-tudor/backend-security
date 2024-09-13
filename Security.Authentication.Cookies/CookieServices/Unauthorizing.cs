using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static int UnauthorizeCookie (HttpContext context) =>
    UnauthorizeAuth (context, ResolveRequiredService<CookieAuthenticationOptions>(context), ResolveCookiesLogger(context));
}