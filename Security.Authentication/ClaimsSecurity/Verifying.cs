
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static bool EqualsClaimValue(Claim? claim, string? value) => string.Equals(claim?.Value, value, StringComparison.Ordinal);
}