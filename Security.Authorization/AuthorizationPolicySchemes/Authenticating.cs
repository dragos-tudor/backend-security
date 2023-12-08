
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class Funcs {

  static IEnumerable<AuthenticateResult> AuthenticatePolicySchemes (
    AuthorizationPolicy policy,
    HttpContext context,
    AuthenticateSchemeFunc authenticateSchemeFunc) =>
      policy
        .AuthenticationSchemes
        .Select(schemeName => authenticateSchemeFunc(context, schemeName));

}