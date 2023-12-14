
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static IEnumerable<string> ChallengePolicySchemes (
    AuthorizationPolicy policy,
    HttpContext context,
    ChallengeSchemeFunc challengeScheme) =>
      policy
        .AuthenticationSchemes
        .Select(schemeName => challengeScheme(context, schemeName));

}