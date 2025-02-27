
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static Claim ApplyJsonClaimMapper(JsonClaimMapper claimMapper, KeyValuePair<string, string> rawClaim, string issuer) =>
    new(claimMapper.ClaimType, rawClaim.Value, "string", issuer);

  public static Claim ApplyClaimMapper(ClaimMapper? claimMapper, KeyValuePair<string, string> rawClaim, string issuer) =>
    claimMapper switch {
      JsonClaimMapper jsonClaimMapper => ApplyJsonClaimMapper(jsonClaimMapper, rawClaim, issuer),
      _ => new Claim(rawClaim.Key, rawClaim.Value, "string", issuer)
    };

  public static IEnumerable<Claim> ApplyClaimMappers(IEnumerable<ClaimMapper> claimMappers, IDictionary<string, string> rawClaims, string issuer) =>
    rawClaims.Select(claim => ApplyClaimMapper(GetClaimMapper(claimMappers, claim.Key), claim, issuer));

  public static IEnumerable<Claim> ApplyClaimMappers(IEnumerable<ClaimMapper> claimMappers, IEnumerable<Claim> claims, string issuer) =>
    claims.Select(claim => ApplyClaimMapper(GetClaimMapper(claimMappers, claim.Type), new KeyValuePair<string, string>(claim.Type, claim.Value), issuer));
}