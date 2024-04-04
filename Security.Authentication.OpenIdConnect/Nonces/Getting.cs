namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string GetProtectedNonce(string cookieName) =>
    cookieName[OpenIdConnectDefaults.CookieNoncePrefix.Length..];
}