using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  static async Task<(AuthenticateResult, PolicyAuthorizationResult)> Authorize (
    HttpContext context,
    IAuthorizationPolicyProvider policyProvider,
    IAuthorizationService authzService)
  {
    var endpoint = MarkEndpointInvoked(context, context.GetEndpoint());
    var policy = await CombinePolicies(policyProvider, endpoint);
    if (policy is null) return (AuthenticateResult.NoResult(), PolicyAuthorizationResult.Success());

    var authResult = GetAuthenticationFeature<AuthenticateResult>(context) ?? GetDefaultAuthenticateResult(context);
    if (IsAnonymousEndpoint(endpoint)) return (authResult, PolicyAuthorizationResult.Success());

    var authzResult = await AuthorizePolicy(policy, authResult, authzService, context, endpoint);
    return (authResult, authzResult);
  }

  static Task<(AuthenticateResult, PolicyAuthorizationResult)> Authorize (HttpContext context) =>
    Authorize(
      context,
      ResolveRequiredService<IAuthorizationPolicyProvider>(context),
      ResolveRequiredService<IAuthorizationService>(context)
    );
}