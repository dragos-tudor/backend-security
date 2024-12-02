
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static IEnumerable<Claim> MapClaim(JsonKeyClaimMapper claimMapper, JsonElement claims, string issuer)
  {
    if (!claims.TryGetProperty(claimMapper.JsonKey, out var jsonClaim)) return [];
    if (jsonClaim.ValueKind == JsonValueKind.Object || jsonClaim.ValueKind == JsonValueKind.Undefined) return [];
    if (jsonClaim.ValueKind == JsonValueKind.Array) return MapClaims(claimMapper, jsonClaim, issuer);

    return [CreateClaim(claimMapper.ClaimType, jsonClaim.ToString()!, issuer)];
  }

  static IEnumerable<Claim> MapClaim(JsonSubKeyClaimMapper claimMapper, JsonElement claims, string issuer)
  {
    if (!claims.TryGetProperty(claimMapper.JsonKey, out JsonElement jsonClaim)) return [];
    if (jsonClaim.ValueKind != JsonValueKind.Object) return [];

    if (!jsonClaim.TryGetProperty(claimMapper.JsonSubKey, out JsonElement jsonSubClaim)) return [];
    if (jsonSubClaim.ValueKind == JsonValueKind.Array) return MapClaims(claimMapper, jsonSubClaim, issuer);

    return [CreateClaim(claimMapper.ClaimType, jsonSubClaim.ToString()!, issuer)];
  }

  static IEnumerable<Claim> MapClaims(ClaimMapper claimMapper, JsonElement claims, string issuer) =>
    claims.EnumerateArray().Select(value => CreateClaim(claimMapper.ClaimType, value.ToString()!, issuer));

  public static IEnumerable<Claim> MapClaim(ClaimMapper claimMapper, JsonElement claims, string issuer) =>
    claimMapper switch {
      JsonSubKeyClaimMapper subKeyMapper => MapClaim(subKeyMapper, claims, issuer),
      JsonKeyClaimMapper keyMapper => MapClaim(keyMapper, claims, issuer),
      _ => []
    };
}