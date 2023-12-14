
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static void SetResponseCookieHeader (
    HttpContext context,
    string protectedTicket,
    ICookieManager cookieManager,
    CookieOptions cookieOptions,
    string cookieName) =>
      AppendAuthenticationCookie(
        context,
        cookieManager,
        cookieName,
        protectedTicket,
        cookieOptions
      );

}