
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static IEnumerable<string> ForbidPolicySchemes (
    AuthorizationPolicy policy,
    HttpContext context,
    ForbidSchemeFunc forbidScheme) =>
      policy
        .AuthenticationSchemes
        .Select(schemeName => forbidScheme(context, schemeName));

}