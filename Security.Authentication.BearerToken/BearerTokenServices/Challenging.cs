using System.Net;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static string ChallengeBearerToken(
    HttpContext context,
    BearerTokenOptions authOptions,
    ILogger logger)
  {
    SetWWWAuthenticateResponseHeader(context, "Bearer");
    SetResponseStatus(context, HttpStatusCode.Unauthorized);

    LogChallenged(logger, authOptions.SchemeName, context.TraceIdentifier);
    return string.Empty;
  }
}