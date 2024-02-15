using static System.Text.Json.JsonDocument;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static ClaimsPrincipal BuildClaimsPrincipal(OAuthOptions authOptions, string userInfo)
  {
    using var userData = Parse(userInfo);
    var principal = CreatePrincipal(authOptions.SchemeName);
    AddClaimsIdentityClaims((ClaimsIdentity)principal.Identity!, userData, authOptions);
    return principal;
  }
}