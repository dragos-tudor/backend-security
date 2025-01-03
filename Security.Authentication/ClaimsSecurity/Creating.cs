
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static Claim CreateClaim(string claimType, string value, string issuer, string valueType = "string") => new(claimType, value, valueType, issuer);

  public static Claim CreateNameClaim(string? userName) => new(ClaimTypes.Name, userName ?? string.Empty);

  public static Claim CreateNameIdClaim(string id) => new(ClaimTypes.NameIdentifier, id);

  public static Claim CreateRoleClaim(string roleName) => new(ClaimTypes.Role, roleName);
}