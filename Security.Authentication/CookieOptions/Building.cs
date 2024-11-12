
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static CookieOptions BuildCookieOptions(HttpContext context, CookieBuilder cookieBuilder) => cookieBuilder.Build(context);
}