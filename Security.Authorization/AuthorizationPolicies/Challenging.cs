
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static string ChallengePolicy (
    AuthorizationPolicy policy,
    HttpContext context,
    ChallengeSchemeFunc challengeScheme) =>
      IsSchemelessPolicy(policy)?
        challengeScheme(context):
        ChallengePolicySchemes(policy, context, challengeScheme).JoinPolicySchemeFailures();
}