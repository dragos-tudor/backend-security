
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Security.Authorization;

partial class Funcs {

  static IEnumerable<ClaimsIdentity> GetAuthenticatedClaimsIdentities (ClaimsPrincipal principal) =>
    principal
      .Identities
      .Where(IsAuthenticatedClaimsIdentity);

}