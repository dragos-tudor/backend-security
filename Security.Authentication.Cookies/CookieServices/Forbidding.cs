
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static string? ForbidCookie (HttpContext context, AuthenticationProperties? authProperties = default) =>
    ForbidAuth (context, ResolveRequiredService<CookieAuthenticationOptions>(context), ResolveCookiesLogger(context), authProperties);
}