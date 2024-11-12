using Microsoft.AspNetCore.Http;
using Security.Authentication;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  static async Task AuthorizationMiddleware(ChallengeFunc challenge, ForbidFunc forbid, HttpContext context, RequestDelegate next)
  {
    var(authResult, authzResult) = await Authorize(context);
    if(IsAuthorizationChallenged(authzResult)) challenge(context, GetAuthenticationProperties(authResult));
    if(IsAuthorizationForbidden(authzResult)) forbid(context, GetAuthenticationProperties(authResult));

    var principal = authResult.Principal;
    if(ExistsPrincipal(principal)) SetContextUser(context, principal);

    if(IsAuthorizationSucceeded(authzResult)) await next(context);
    if(!IsAuthorizationSucceeded(authzResult)) await WriteResponse(context,string.Empty);
  }
}