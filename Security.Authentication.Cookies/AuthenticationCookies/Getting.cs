
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static string? GetAuthenticationCookie (HttpContext context, ICookieManager cookieManager, string cookieName) =>
    cookieManager.GetRequestCookie(context, cookieName);
}