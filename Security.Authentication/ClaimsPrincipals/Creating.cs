
using System.Collections.Generic;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  public static ClaimsPrincipal CreateClaimsPrincipal (string schemeName, IEnumerable<Claim>? claims = default) =>
    new(CreateClaimsIdentity(schemeName, claims));

}