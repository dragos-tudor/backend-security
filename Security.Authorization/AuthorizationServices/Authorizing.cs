
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static async Task<(PolicyAuthorizationResult?, ClaimsPrincipal?)> AuthorizeAsync (
    AuthenticateSchemeFunc authenticateScheme,
    ChallengeSchemeFunc challengeScheme,
    ForbidSchemeFunc forbidScheme,
    IAuthorizationPolicyProvider policyProvider,
    IAuthorizationService authorizationService,
    HttpContext context)
  {
    var endpoint = MarkEndpointInvoked(context, context.GetEndpoint());
    var policy = await CombinePolicies(policyProvider, endpoint);
    if (policy is null) return (default, default);

    var authenticateResult = AuthenticatePolicy(policy, authenticateScheme, context);
    if (IsAnonymousEndpoint(endpoint)) return (default, authenticateResult.Principal);

    var authorizationResult = await AuthorizePolicyAsync(policy, authenticateResult, authorizationService, context, endpoint);
    if (authorizationResult.Forbidden) ForbidPolicy(policy, context, forbidScheme);
    if (authorizationResult.Challenged) ChallengePolicy(policy, context, challengeScheme);

    return (authorizationResult, authenticateResult.Principal);
  }

}