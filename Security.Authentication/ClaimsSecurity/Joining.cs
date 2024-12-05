
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  const string ShortClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/ShortTypeName";

  public static IEnumerable<Claim> JoinUniqueClaims(IEnumerable<Claim> claims, IEnumerable<Claim> otherClaims) =>
    claims.Concat(otherClaims.Where(otherClaim => !claims.Any(claim => EqualsClaim(claim, otherClaim))));
}