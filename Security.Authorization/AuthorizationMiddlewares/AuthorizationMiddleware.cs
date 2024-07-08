
using Microsoft.AspNetCore.Http;
using Security.Authentication;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static async Task AuthorizationMiddleware (
    ChallengeFunc challenge,
    ForbidFunc forbid,
    HttpContext context,
    RequestDelegate next)
  {
    var (authResult, principal) =
      await Authorize(context, challenge, forbid);

    if (ExistsPrincipal(principal)) SetContextUser(context, principal);
    if (IsSuccessfulAuthorization(authResult)) await next(context);
    if (!IsSuccessfulAuthorization(authResult)) await WriteResponse(context,string.Empty);
  }

}