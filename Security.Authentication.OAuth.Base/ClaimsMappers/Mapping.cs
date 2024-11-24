
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static IEnumerable<Claim> MapClaim(JsonKeyClaimMapper claimMapper, JsonElement jsonData, string issuer)
  {
    if (!jsonData.TryGetProperty(claimMapper.JsonKey, out var jsonValue)) return [];
    if (jsonValue.ValueKind == JsonValueKind.Object || jsonValue.ValueKind == JsonValueKind.Undefined) return [];
    if (jsonValue.ValueKind == JsonValueKind.Array) return MapClaims(claimMapper, jsonValue, issuer);

    return [CreateClaim(claimMapper.ClaimType, jsonValue.ToString()!, issuer)];
  }

  static IEnumerable<Claim> MapClaim(JsonSubKeyClaimMapper claimMapper, JsonElement jsonData, string issuer)
  {
    if (!jsonData.TryGetProperty(claimMapper.JsonKey, out JsonElement jsonValue)) return [];
    if (jsonValue.ValueKind != JsonValueKind.Object) return [];

    if (!jsonValue.TryGetProperty(claimMapper.JsonSubKey, out JsonElement jsonSubValue)) return [];
    if (jsonSubValue.ValueKind == JsonValueKind.Array) return MapClaims(claimMapper, jsonSubValue, issuer);

    return [CreateClaim(claimMapper.ClaimType, jsonSubValue.ToString()!, issuer)];
  }

  static IEnumerable<Claim> MapClaims(ClaimMapper claimMapper, JsonElement jsonData, string issuer) =>
    jsonData.EnumerateArray().Select(value => CreateClaim(claimMapper.ClaimType, value.ToString()!, issuer));

  public static IEnumerable<Claim> MapClaim(ClaimMapper claimMapper, JsonElement jsonData, string issuer) =>
    claimMapper switch {
      JsonSubKeyClaimMapper subKeyMapper => MapClaim(subKeyMapper, jsonData, issuer),
      JsonKeyClaimMapper keyMapper => MapClaim(keyMapper, jsonData, issuer),
      _ => []
    };
}