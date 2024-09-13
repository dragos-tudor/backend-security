
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using static System.Net.HttpStatusCode;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  public static string? ChallengeAuthorization<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions authOptions
  ) where TOptions: Authentication.AuthenticationOptions
  {
    LogChallenged(ResolveAuthorizationLogger(context), authOptions.SchemeName, context.TraceIdentifier);
    SetResponseStatus(context, Unauthorized);
    return default;
  }

  public static string? ChallengeAuthorization<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties
  ) where TOptions: Authentication.AuthenticationOptions
   =>
      ChallengeAuthorization(
        context,
        authProperties,
        ResolveRequiredService<TOptions>(context));
}