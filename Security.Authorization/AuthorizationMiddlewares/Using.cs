
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authorization;

partial class Funcs {

  static T ResolveService<T> (HttpContext context) where T : notnull =>
    context.RequestServices.GetRequiredService<T>();

  public static IApplicationBuilder UseSchemeAuthorization (
    this IApplicationBuilder builder,
    AuthenticateSchemeFunc authenticateSchemeFunc,
    ChallengeSchemeFunc challengeSchemeFunc,
    ForbidSchemeFunc forbidSchemeFunc) =>
      builder.Use((context, next) =>
        SchemeAuthorizationMiddleware(
          authenticateSchemeFunc,
          challengeSchemeFunc,
          forbidSchemeFunc,
          ResolveService<IAuthorizationPolicyProvider>(context),
          ResolveService<IAuthorizationService>(context),
          context,
          next));

}