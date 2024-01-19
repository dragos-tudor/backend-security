namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal static bool IsClaimTypesEquality(string type, string claimType) =>
    string.Equals(type, claimType, StringComparison.OrdinalIgnoreCase);

  internal static bool IsClaimWithType(Claim claim, string claimType) =>
    string.Equals(claim.Type, claimType, StringComparison.OrdinalIgnoreCase);

  internal static bool IsDuplicateClaim(Claim? claim, string value) =>
    claim != null && string.Equals(claim.Value, value, StringComparison.Ordinal);
}