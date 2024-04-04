
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static void DeleteAuthenticationCookie (HttpContext context, ICookieManager cookieManager, string cookieName, CookieOptions cookieOptions) =>
    cookieManager.DeleteCookie(context, cookieName, cookieOptions);

}