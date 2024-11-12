
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static DateTimeOffset? SetCookieOptionsExpires(CookieOptions cookieOptions, DateTimeOffset? expires) => cookieOptions.Expires = expires;

  public static string? SetCookieOptionsPath(CookieOptions cookieOptions, string? path) => cookieOptions.Path = path;

  public static bool SetCookieOptionsSecure(CookieOptions cookieOptions, bool secure) =>  cookieOptions.Secure = secure;
}