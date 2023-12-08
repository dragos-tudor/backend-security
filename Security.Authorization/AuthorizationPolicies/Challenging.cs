
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class Funcs {

  static string ChallengePolicy (
    AuthorizationPolicy policy,
    HttpContext context,
    ChallengeSchemeFunc challengeSchemeFunc) =>
      IsSchemelessPolicy(policy)?
        challengeSchemeFunc(context):
        ChallengePolicySchemes(policy, context, challengeSchemeFunc).JoinPolicySchemeFailures();
}