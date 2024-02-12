
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? GetNonceCookieName(IRequestCookieCollection cookies, string? cookiePrefix) =>
    cookies.Keys.FirstOrDefault(cookieName => IsNonceCookieName(cookieName, cookiePrefix));

  static string GetProtectedNonce(string cookieName, string cookiePrefix) =>
    cookieName[cookiePrefix.Length..];
}