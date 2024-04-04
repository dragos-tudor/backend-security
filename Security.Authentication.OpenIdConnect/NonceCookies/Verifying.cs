
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsNonceCookieName(string cookieName) =>
    cookieName.StartsWith(OpenIdConnectDefaults.CookieNoncePrefix, StringComparison.Ordinal);
}