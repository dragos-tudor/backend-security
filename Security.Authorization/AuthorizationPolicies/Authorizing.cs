
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static async Task<PolicyAuthorizationResult> AuthorizePolicy(
    AuthorizationPolicy policy,
    AuthenticateResult authResult,
    IAuthorizationService authzService,
    HttpContext context,
    Endpoint? endpoint)
  {
    var resource =(object?)(IsEndpointResource()? endpoint: context);
    var authzResult = await authzService.AuthorizeAsync(context.User, resource, policy);

    if (authzResult.Succeeded) return PolicyAuthorizationResult.Success();
    if (authResult.Succeeded) return PolicyAuthorizationResult.Forbid(authzResult.Failure);
    return PolicyAuthorizationResult.Challenge();
  }

}