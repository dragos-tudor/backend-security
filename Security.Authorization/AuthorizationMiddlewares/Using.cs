
using Microsoft.AspNetCore.Builder;
using Security.Authentication;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  public static IApplicationBuilder UseAuthorization (
    this IApplicationBuilder builder,
    ChallengeFunc challenge,
    ForbidFunc forbid) =>
      builder.Use((context, next) =>
        AuthorizationMiddleware(challenge, forbid, context, next));

}