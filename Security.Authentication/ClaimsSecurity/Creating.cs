
namespace Security.Authentication;

partial class Funcs {

  public static Claim CreateNameClaim (string userName) =>
    new(ClaimTypes.Name, userName);

  public static Claim CreateNameIdClaim (string id) =>
    new (ClaimTypes.NameIdentifier, id);

  public static Claim CreateRoleClaim (string roleName) =>
    new (ClaimTypes.Role, roleName);

}