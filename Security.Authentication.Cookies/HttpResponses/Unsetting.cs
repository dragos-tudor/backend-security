
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static void UnsetResponseCookieHeader (
    HttpContext context,
    ICookieManager cookieManager,
    CookieOptions cookieOptions,
    string cookieName) =>
      DeleteAuthenticationCookie(
        context,
        cookieManager,
        cookieName,
        cookieOptions
      );

}