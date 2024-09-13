
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static string ForbidCookie (HttpContext context, AuthenticationProperties authProperties) =>
    ForbidAuth (context, authProperties, ResolveRequiredService<CookieAuthenticationOptions>(context), ResolveCookiesLogger(context));
}