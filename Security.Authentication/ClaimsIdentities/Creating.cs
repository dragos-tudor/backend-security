
using System.Collections.Generic;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  static ClaimsIdentity CreateClaimsIdentity (string schemeName, IEnumerable<Claim>? claims = default) =>
    new(claims, schemeName);

}