
using Microsoft.AspNetCore.Http;
using static System.Net.HttpStatusCode;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static int UnauthenticateAuth<TOptions> (HttpContext context, TOptions authOptions, ILogger logger) where TOptions: AuthenticationOptions
  {
    SetResponseStatus(context, Unauthorized);
    LogChallenged(logger, authOptions.SchemeName, context.TraceIdentifier);
    return (int)Unauthorized;
  }
}