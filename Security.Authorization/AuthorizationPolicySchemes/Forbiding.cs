
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class Funcs {

  static IEnumerable<string> ForbidPolicySchemes (
    AuthorizationPolicy policy,
    HttpContext context,
    ForbidSchemeFunc forbidSchemeFunc) =>
      policy
        .AuthenticationSchemes
        .Select(schemeName => forbidSchemeFunc(context, schemeName));

}