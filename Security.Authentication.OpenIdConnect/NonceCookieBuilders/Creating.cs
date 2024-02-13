using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static CookieBuilder CreateNonceCookieBuilder(
    HttpRequest request,
    OpenIdConnectOptions oidcOptions) =>
      new ()
      {
        Name = OpenIdConnectDefaults.CookieNoncePrefix,
        HttpOnly = true,
        SameSite = SameSiteMode.None,
        SecurePolicy = CookieSecurePolicy.Always,
        Path = BuildNonceCookiePath(request, oidcOptions.CallbackPath),
        IsEssential = true
      };
}