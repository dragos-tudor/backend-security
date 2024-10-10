
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static string ForbidFacebook (
    HttpContext context,
    FacebookOptions authOptions,
    ILogger logger)
  {
    SetResponseStatus(context, HttpStatusCode.Forbidden);

    LogChallenged(logger, authOptions.SchemeName, context.TraceIdentifier);
    return string.Empty;
  }

  public static string ForbidFacebook (HttpContext context) =>
    ForbidFacebook (
      context,
      ResolveRequiredService<FacebookOptions>(context),
      ResolveFacebookLogger(context));
}