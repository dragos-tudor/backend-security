using static System.Text.Json.JsonDocument;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static ClaimsPrincipal BuildClaimsPrincipal(OpenIdConnectOptions authOptions, ClaimsIdentity identity, string userInfo)
  {
    using var userData = Parse(userInfo);
    AddClaimsIdentityClaims(identity, userData, authOptions);
    return CreatePrincipal(identity);
  }
}