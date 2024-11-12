
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static CookieOptions BuildCookieOptions(AuthenticationProperties authProperties, HttpContext context)
  {
    var cookieOptions = CreateCookieOptions();
    var cookieOptionsExpire = IsAuthenticationPropertiesPersistent(authProperties!) ? GetAuthenticationPropertiesExpires(authProperties!) : default;

    SetCookieOptionsExpires(cookieOptions, cookieOptionsExpire);
    SetCookieOptionsSecure(cookieOptions, IsSecuredCookie(context, CookieSecurePolicy.SameAsRequest));
    return cookieOptions;
  }
}