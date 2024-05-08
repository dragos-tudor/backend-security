using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Security.Authentication;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static async Task<(PolicyAuthorizationResult?, ClaimsPrincipal?)> Authorize (
    HttpContext context,
    ChallengeFunc challenge,
    ForbidFunc forbid,
    IAuthorizationPolicyProvider policyProvider,
    IAuthorizationService authorizationService)
  {
    var endpoint = MarkEndpointInvoked(context, context.GetEndpoint());
    var policy = await CombinePolicies(policyProvider, endpoint);
    if (policy is null) return (default, default);

    var authResult = GetAuthenticationFeature<AuthenticateResult>(context) ?? GetDefaultAuthenticateResult(context);
    if (IsAnonymousEndpoint(endpoint)) return (default, authResult.Principal);

    var authzResult = await AuthorizePolicy(policy, authResult, authorizationService, context, endpoint);
    if (authzResult.Forbidden) forbid(context, GetAuthenticationProperties(authResult));
    if (authzResult.Challenged) challenge(context, GetAuthenticationProperties(authResult));

    return (authzResult, authResult.Principal);
  }

  static Task<(PolicyAuthorizationResult?, ClaimsPrincipal?)> Authorize (
    HttpContext context,
    ChallengeFunc challenge,
    ForbidFunc forbid) =>
      Authorize(
        context,
        challenge,
        forbid,
        ResolveService<IAuthorizationPolicyProvider>(context),
        ResolveService<IAuthorizationService>(context)
      );

}