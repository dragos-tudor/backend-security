
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static string ForbidGoogle (
    HttpContext context,
    GoogleOptions authOptions,
    ILogger logger)
  {
    SetResponseStatus(context, HttpStatusCode.Forbidden);

    LogChallenged(logger, authOptions.SchemeName, context.TraceIdentifier);
    return string.Empty;
  }

  public static string ForbidGoogle (HttpContext context) =>
    ForbidGoogle (
      context,
      ResolveRequiredService<GoogleOptions>(context),
      ResolveGoogleLogger(context));
}