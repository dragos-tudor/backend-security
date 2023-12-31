
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static ClaimsPrincipal BuildClaimsPrincipalWithClaims (
    JsonDocument responseJson,
    ClaimActionCollection claimActions,
    string claimsIssuer,
    string schemeName)
  {
    var claimsPrincipal = CreatePrincipal(schemeName);
    var claimsIdentity = (ClaimsIdentity)claimsPrincipal.Identity!;
    foreach (var claimAction in claimActions)
      claimAction.Run(responseJson.RootElement, claimsIdentity, claimsIssuer);
    return claimsPrincipal;
  }

}