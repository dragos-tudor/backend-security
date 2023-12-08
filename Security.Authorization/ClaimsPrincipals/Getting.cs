
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Security.Authorization;

partial class Funcs {

  static IEnumerable<ClaimsPrincipal> GetAuthenticatedClaimsPrincipals (IEnumerable<ClaimsPrincipal?> principals) =>
    principals
      .Where(principal => principal is not null)
      .Where(principal => principal!.Identity?.IsAuthenticated ?? false)!;

}