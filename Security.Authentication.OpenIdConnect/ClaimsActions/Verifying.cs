
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool EqualsClaimType(ClaimAction claimAction, string type) => string.Equals(claimAction.ClaimType, type, StringComparison.OrdinalIgnoreCase);
}