
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  public static IApplicationBuilder UseAuthorization (
    this IApplicationBuilder builder,
    AuthenticateSchemeFunc authenticateScheme,
    ChallengeSchemeFunc challengeScheme,
    ForbidSchemeFunc forbidScheme) =>
      builder.Use((context, next) =>
        AuthorizationMiddleware(
          authenticateScheme,
          challengeScheme,
          forbidScheme,
          context.RequestServices.GetRequiredService<IAuthorizationPolicyProvider>(),
          context.RequestServices.GetRequiredService<IAuthorizationService>(),
          context,
          next));

}