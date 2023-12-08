
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class Funcs {

  static async Task<(PolicyAuthorizationResult?, ClaimsPrincipal?)> AuthorizeAsync (
    AuthenticateSchemeFunc authenticateSchemeFunc,
    ChallengeSchemeFunc challengeSchemeFunc,
    ForbidSchemeFunc forbidSchemeFunc,
    IAuthorizationPolicyProvider policyProvider,
    IAuthorizationService authorizationService,
    HttpContext context)
  {
    var endpoint = MarkEndpointInvoked(context, context.GetEndpoint());
    var policy = await CombinePolicies(policyProvider, endpoint);
    if (policy is null) return (default, default);

    var authenticateResult = AuthenticatePolicy(policy, authenticateSchemeFunc, context);
    if (IsAnonymousEndpoint(endpoint)) return (default, authenticateResult.Principal);

    var authorizationResult = await AuthorizePolicyAsync(policy, authenticateResult, authorizationService, context, endpoint);
    if (authorizationResult.Forbidden) ForbidPolicy(policy, context, forbidSchemeFunc);
    if (authorizationResult.Challenged) ChallengePolicy(policy, context, challengeSchemeFunc);

    return (authorizationResult, authenticateResult.Principal);
  }

}