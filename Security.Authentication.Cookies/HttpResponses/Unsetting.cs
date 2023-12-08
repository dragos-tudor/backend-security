
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class Funcs {

  static void UnsetResponseCookieHeader (
    HttpContext context,
    CookieAuthenticationOptions authOptions,
    CookieOptions cookieOptions,
    string cookieName) =>
      DeleteAuthenticationCookie(
        context,
        authOptions.CookieManager,
        cookieName,
        cookieOptions
      );

}