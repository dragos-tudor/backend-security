
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static ClaimsPrincipal CombineClaimsPrincipal (ClaimsPrincipal? firstPrincipal, ClaimsPrincipal? secondPrincipal) =>
    firstPrincipal is null && secondPrincipal is not null?
      secondPrincipal:
      CreateClaimsPrincipal()
        .AddAuthenticatedClaimsIdentities(firstPrincipal)
        .AddClaimsIdentities(secondPrincipal);

  static ClaimsPrincipal? CombineClaimsPrincipals (IEnumerable<ClaimsPrincipal> principals) =>
    principals.Aggregate(
      default(ClaimsPrincipal),
      CombineClaimsPrincipal);

}