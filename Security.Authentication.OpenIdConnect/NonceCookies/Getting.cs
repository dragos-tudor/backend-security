
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string GetNonceCookieName(string? name, string suffix) =>
    $"{name}{suffix}";
}