
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static void AppendAuthenticationCookie (HttpContext context, ICookieManager cookieManager, string cookieName, string cookieContent, CookieOptions cookieOptions) =>
    cookieManager.AppendResponseCookie(context, cookieName, cookieContent, cookieOptions);

}