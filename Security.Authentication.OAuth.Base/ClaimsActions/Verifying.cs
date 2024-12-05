
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static bool EqualsClaimType(ClaimAction claimAction, string type) => string.Equals(claimAction.ClaimType, type, StringComparison.OrdinalIgnoreCase);
}