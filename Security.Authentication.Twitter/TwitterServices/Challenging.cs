
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static string ChallengeTwitter (
    HttpContext context,
    TwitterOptions authOptions,
    ILogger logger)
  {
    SetResponseStatus(context, HttpStatusCode.Unauthorized);

    LogChallenged(logger, authOptions.SchemeName, context.TraceIdentifier);
    return string.Empty;
  }

  public static string ChallengeTwitter (HttpContext context) =>
    ChallengeTwitter (
      context,
      ResolveRequiredService<TwitterOptions>(context),
      ResolveTwitterLogger(context));
}