
using static System.Text.Json.JsonDocument;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static ClaimsPrincipal AddClaimsPrincipalClaims (
    ClaimsPrincipal principal,
    OAuthOptions authOptions,
    string jsonContent)
  {
    using var jsonDocument = Parse(jsonContent);
    var claimsIssuer = GetClaimsIssuer(authOptions);

    foreach (var claimAction in authOptions.ClaimActions)
      claimAction.Run(jsonDocument.RootElement, (ClaimsIdentity)principal.Identity!, claimsIssuer!);
    return principal;
  }

}