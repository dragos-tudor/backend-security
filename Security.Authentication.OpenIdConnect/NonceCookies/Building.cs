
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string BuildNonceCookieName(string nonce) =>
    $"{OpenIdConnectDefaults.CookieNoncePrefix}{nonce}";

  static string? BuildNonceCookiePath(HttpRequest request, string additionalPath) =>
    $"{GetRequestPathBase(request)}{additionalPath}";
}