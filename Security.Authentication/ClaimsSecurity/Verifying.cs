
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  static bool EqualsClaimValue(Claim claim, Claim otherClaim) => string.Equals(claim.Value, otherClaim.Value, StringComparison.Ordinal);

  static bool EqualsClaimType(Claim claim, Claim otherClaim) =>
    string.Equals(claim.Type, otherClaim.Type, StringComparison.Ordinal) ||
    (claim.Properties.TryGetValue(ShortClaimType, out var shortClaimType) && string.Equals(shortClaimType, otherClaim.Type, StringComparison.Ordinal));

  public static bool EqualsClaim(Claim claim, Claim otherClaim) => EqualsClaimType(claim, otherClaim) && EqualsClaimValue(claim, otherClaim);
}