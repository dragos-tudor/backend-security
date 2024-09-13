
using Microsoft.AspNetCore.Builder;
using Security.Authentication;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  public static IApplicationBuilder UseAuthorization (this IApplicationBuilder builder, ChallengeFunc challenge, ForbidFunc forbid) =>
    builder.Use((context, next) => AuthorizationMiddleware(challenge, forbid, context, next));

  // TODO: extend middlewares
  // public static IApplicationBuilder UseAuthorization (this IApplicationBuilder builder, UnauthenticateFunc unauthenticate, UnauthorizeFunc unauthorize) =>
  // builder.Use((context, next) => AuthorizationMiddleware(unauthenticate, unauthorize, context, next));
}