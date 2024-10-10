
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static string ChallengeGoogle (
    HttpContext context,
    GoogleOptions authOptions,
    ILogger logger)
  {
    SetResponseStatus(context, HttpStatusCode.Unauthorized);

    LogChallenged(logger, authOptions.SchemeName, context.TraceIdentifier);
    return string.Empty;
  }

  public static string ChallengeGoogle (HttpContext context) =>
    ChallengeGoogle (
      context,
      ResolveRequiredService<GoogleOptions>(context),
      ResolveGoogleLogger(context));
}