
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static Claim ApplyJsonClaimMapper(JsonClaimMapper claimMapper, KeyValuePair<string, string> rawClaim, string issuer) =>
    new Claim(claimMapper.ClaimType, rawClaim.Value, "string", issuer);

  public static Claim ApplyClaimMapper(ClaimMapper? claimMapper, KeyValuePair<string, string> rawClaim, string issuer) =>
    claimMapper switch {
      JsonClaimMapper jsonClaimMapper => ApplyJsonClaimMapper(jsonClaimMapper, rawClaim, issuer),
      _ => new Claim(rawClaim.Key, rawClaim.Value, "string", issuer)
    };

  public static IEnumerable<Claim> ApplyClaimMappers(IEnumerable<ClaimMapper> claimMappers, IDictionary<string, string> rawClaims, string issuer) =>
    rawClaims.Select(claim => ApplyClaimMapper(GetClaimMapper(claimMappers, claim), claim, issuer));
}