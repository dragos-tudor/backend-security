
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static IEnumerable<Claim> RunClaimAction(DeleteKeyClaimAction claimAction, ClaimsIdentity identity)
  {
    foreach (var claim in identity.FindAll(claimAction.ClaimType))
      if (identity.TryRemoveClaim(claim)) yield return claim;
  }

  public static Claim? RunClaimAction(UniqueKeyClaimAction claimAction, ClaimsIdentity identity, JsonElement jsonData, string issuer)
  {
    var jsonValue = jsonData.GetString(claimAction.JsonKey);
    if (IsEmptyString(jsonValue)) return default;

    var claim = identity.FindFirst(claim => EqualsClaimType(claimAction, claim.Type));
    if (EqualsClaimValue(claim, jsonValue)) return default;

    var jwtClaim = identity.FindFirst(claim =>
      claim.Properties.TryGetValue(JwtSecurityTokenHandler.ShortClaimTypeProperty, out var shortType) && EqualsClaimType(claimAction, shortType)
    );
    if (EqualsClaimValue(jwtClaim, jsonValue)) return default;

    var newClaim = CreateClaim(claimAction.ClaimType, jsonValue!, claimAction.ClaimValueType, issuer);
    identity.AddClaim(newClaim);

    return newClaim;
  }
}