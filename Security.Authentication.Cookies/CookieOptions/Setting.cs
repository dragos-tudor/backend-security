
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static bool SetCookieOptionsSecure (CookieOptions cookieOptions, bool secure) =>
    cookieOptions.Secure = secure;

  static DateTimeOffset? SetCookieOptionsExpires (CookieOptions cookieOptions, DateTimeOffset? expires) =>
    cookieOptions.Expires = expires;

}