using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static NonceCookieBuilder CreateNonceCookieBuilder(OpenIdConnectOptions oidcOptions) =>
    new (oidcOptions)
    {
      Name = OpenIdConnectDefaults.CookieNoncePrefix,
      HttpOnly = true,
      SameSite = SameSiteMode.None,
      SecurePolicy = CookieSecurePolicy.Always,
      IsEssential = true
    };
}