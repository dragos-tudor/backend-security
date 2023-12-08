
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class Funcs {

  static IEnumerable<string> ChallengePolicySchemes (
    AuthorizationPolicy policy,
    HttpContext context,
    ChallengeSchemeFunc challengeSchemeFunc) =>
      policy
        .AuthenticationSchemes
        .Select(schemeName => challengeSchemeFunc(context, schemeName));

}