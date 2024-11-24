
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static string CleanAuthenticationCookie(
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    ICookieManager cookieManager)
  {
    var cookieName = GetAuthenticationCookieName(authOptions);
    var cookieOptions = CreateCookieOptions();
    DeleteCookie(context, cookieManager, cookieName, cookieOptions);

    return cookieName;
  }
}