
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static string ForbidTwitter (
    HttpContext context,
    TwitterOptions authOptions,
    ILogger logger)
  {
    SetResponseStatus(context, HttpStatusCode.Forbidden);

    LogChallenged(logger, authOptions.SchemeName, context.TraceIdentifier);
    return string.Empty;
  }

  public static string ForbidTwitter (HttpContext context) =>
    ForbidTwitter (
      context,
      ResolveRequiredService<TwitterOptions>(context),
      ResolveTwitterLogger(context));
}