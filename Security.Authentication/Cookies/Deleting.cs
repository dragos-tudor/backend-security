
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static void DeleteCookie(HttpContext context, ICookieManager cookieManager, string cookieName, CookieOptions cookieOptions) =>
    cookieManager.DeleteCookie(context, cookieName, cookieOptions);

  public static void DeleteCookie(HttpResponse response, string cookieName, CookieOptions cookieOptions) =>
    response.Cookies.Delete(cookieName, cookieOptions);
}