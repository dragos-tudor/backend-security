
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? GetNonceCookieName(IRequestCookieCollection cookies) =>
    cookies.Keys.FirstOrDefault(IsNonceCookieName);
}