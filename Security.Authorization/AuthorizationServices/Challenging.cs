
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using static System.Net.HttpStatusCode;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  public static string? ChallengeAuth<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions authOptions
  ) where TOptions: Authentication.AuthenticationOptions
  {
    LogChallenged(Logger, authOptions.SchemeName, context.TraceIdentifier);
    SetResponseStatus(context, Unauthorized);
    return default;
  }

  public static string? ChallengeAuth<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties
  ) where TOptions: Authentication.AuthenticationOptions
   =>
      ChallengeAuth(
        context,
        authProperties,
        ResolveService<TOptions>(context));
}