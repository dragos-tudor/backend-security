
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static CookieOptions BuildCookieOptions(CookieBuilder cookieBuilder, AuthenticationProperties authProperties, HttpContext context)
  {
    var cookieOptions = cookieBuilder.Build(context);
    var cookieOptionsExpire = IsAuthenticationPropertiesPersistent(authProperties!) ? GetAuthenticationPropertiesExpires(authProperties!) : default;
    SetCookieOptionsExpires(cookieOptions, cookieOptionsExpire);
    SetCookieOptionsSecure(cookieOptions, IsSecuredCookie(context, cookieBuilder.SecurePolicy));
    return cookieOptions;
  }

}