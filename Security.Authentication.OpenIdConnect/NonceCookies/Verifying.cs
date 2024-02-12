
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsNonceCookieName(string cookieName, string? cookiePrefix) =>
    cookieName.StartsWith(cookiePrefix ?? string.Empty, StringComparison.Ordinal);
}