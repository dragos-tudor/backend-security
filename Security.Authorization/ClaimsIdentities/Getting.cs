
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static IEnumerable<ClaimsIdentity> GetAuthenticatedClaimsIdentities (ClaimsPrincipal principal) =>
    principal
      .Identities
      .Where(IsAuthenticatedClaimsIdentity);

}