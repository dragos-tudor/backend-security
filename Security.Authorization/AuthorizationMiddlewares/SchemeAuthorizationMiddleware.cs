
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class Funcs {

  static async Task SchemeAuthorizationMiddleware (
    AuthenticateSchemeFunc authenticateSchemeFunc,
    ChallengeSchemeFunc challengeSchemeFunc,
    ForbidSchemeFunc forbidSchemeFunc,
    IAuthorizationPolicyProvider policyProvider,
    IAuthorizationService authorizationService,
    HttpContext context,
    RequestDelegate next)
  {
    var (authorizationResult, authorizedPrincipal) =
      await AuthorizeAsync(
        authenticateSchemeFunc,
        challengeSchemeFunc,
        forbidSchemeFunc,
        policyProvider,
        authorizationService,
        context);

    if (authorizedPrincipal is not null) SetContextUser(context, authorizedPrincipal);
    if (IsSuccessfulAuthorization(authorizationResult)) await next(context);
  }

}