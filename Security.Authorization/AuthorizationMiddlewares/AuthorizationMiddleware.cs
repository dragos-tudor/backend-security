
using Microsoft.AspNetCore.Http;
using Security.Authentication;
using static Security.Authentication.AuthenticationFuncs;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static async Task AuthorizationMiddleware (
    ChallengeFunc challenge,
    ForbidFunc forbid,
    HttpContext context,
    RequestDelegate next)
  {
    var (authorizationResult, authorizedPrincipal) =
      await Authorize(context, challenge, forbid);

    SetContextUser(context, authorizedPrincipal);
    if (IsSuccessfulAuthorization(authorizationResult)) await next(context);
  }

}