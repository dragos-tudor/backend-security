
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static CookieOptions BuildCookieOptions(
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    DateTimeOffset currentUtc)
  {
    var cookieOptions = CreateCookieOptions();
    var cookieOptionsExpires = GetCookieOptionsExpires(currentUtc, authOptions.ExpireAfter);

    SetCookieOptionsExpires(cookieOptions, cookieOptionsExpires);
    SetCookieOptionsSecure(cookieOptions, IsSecuredCookie(context, CookieSecurePolicy.SameAsRequest));
    return cookieOptions;
  }
}