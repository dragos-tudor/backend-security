
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string BuildNonceCookieName(string? cookiePrefix, string protectedNonce) =>
    $"{cookiePrefix}{protectedNonce}";
}