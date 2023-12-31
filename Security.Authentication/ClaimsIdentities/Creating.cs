
using System.Collections.Generic;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  static ClaimsIdentity CreateIdentity (string schemeName, IEnumerable<Claim>? claims = default) =>
    new(claims, schemeName);

}