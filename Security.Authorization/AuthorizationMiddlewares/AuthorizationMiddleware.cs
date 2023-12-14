
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static async Task AuthorizationMiddleware (
    AuthenticateSchemeFunc authenticateScheme,
    ChallengeSchemeFunc challengeScheme,
    ForbidSchemeFunc forbidScheme,
    IAuthorizationPolicyProvider policyProvider,
    IAuthorizationService authorizationService,
    HttpContext context,
    RequestDelegate next)
  {
    var (authorizationResult, authorizedPrincipal) =
      await AuthorizeAsync(
        authenticateScheme,
        challengeScheme,
        forbidScheme,
        policyProvider,
        authorizationService,
        context);

    SetContextUser(context, authorizedPrincipal);
    if (IsSuccessfulAuthorization(authorizationResult)) await next(context);
  }

}