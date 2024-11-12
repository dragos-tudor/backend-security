
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static void AppendCookie(HttpContext context, ICookieManager cookieManager, string cookieName, string cookieContent, CookieOptions cookieOptions) =>
    cookieManager.AppendResponseCookie(context, cookieName, cookieContent, cookieOptions);

  public static void AppendCookie(HttpResponse response, string cookieName, string cookieContent, CookieOptions cookieOptions) =>
    response.Cookies.Append(cookieName, cookieContent, cookieOptions);
}