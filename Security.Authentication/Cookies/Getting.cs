
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? GetCookie(HttpContext context, ICookieManager cookieManager, string cookieName) => cookieManager.GetRequestCookie(context, cookieName);

  public static string? GetCookiePath(HttpRequest request, string cookiePath) => $"{request.PathBase}{cookiePath}";

  public static string? GetCookieContent(HttpRequest request, string cookieName) => request.Cookies[cookieName];
}