
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static string ChallengeFacebook (
    HttpContext context,
    FacebookOptions authOptions,
    ILogger logger)
  {
    SetResponseStatus(context, HttpStatusCode.Unauthorized);

    LogChallenged(logger, authOptions.SchemeName, context.TraceIdentifier);
    return string.Empty;
  }

  public static string ChallengeFacebook (HttpContext context) =>
    ChallengeFacebook (
      context,
      ResolveRequiredService<FacebookOptions>(context),
      ResolveFacebookLogger(context));
}