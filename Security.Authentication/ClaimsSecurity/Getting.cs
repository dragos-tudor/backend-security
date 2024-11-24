
using System.Collections.Generic;
using System.Linq;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static Claim? GetNameClaim(IEnumerable<Claim>? claims) => claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);

  public static Claim? GetNameIdClaim(IEnumerable<Claim>? claims) => claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

  public static IEnumerable<Claim> GetRoleClaims(IEnumerable<Claim>? claims) => claims?.Where(claim => claim.Type == ClaimTypes.Role) ?? [];
}