
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static IEnumerable<AuthenticateResult> AuthenticatePolicySchemes (
    AuthorizationPolicy policy,
    HttpContext context,
    AuthenticateSchemeFunc authenticateScheme) =>
      policy
        .AuthenticationSchemes
        .Select(schemeName => authenticateScheme(context, schemeName));

}