
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class Funcs {

  static async Task<PolicyAuthorizationResult> AuthorizePolicyAsync (
    AuthorizationPolicy policy,
    AuthenticateResult authenticateResult,
    IAuthorizationService authorizationService,
    HttpContext context,
    Endpoint? endpoint)
  {
    var resource = (object?) (IsEndpointResource()? endpoint: context);
    var authorizationResult = await authorizationService.AuthorizeAsync(context.User, resource, policy);

    return authorizationResult.Succeeded?
      PolicyAuthorizationResult.Success():
      authenticateResult.Succeeded?
        PolicyAuthorizationResult.Forbid(authorizationResult.Failure):
        PolicyAuthorizationResult.Challenge();
  }

}