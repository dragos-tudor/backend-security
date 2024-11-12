using System.Net;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static string ForbidBearerToken(
    HttpContext context,
    BearerTokenOptions authOptions,
    ILogger logger)
  {
    SetResponseStatus(context, HttpStatusCode.Forbidden);

    LogForbidden(logger, authOptions.SchemeName, context.TraceIdentifier);
    return string.Empty;
  }

  public static string ForbidBearerToken(HttpContext context) =>
    ForbidBearerToken(
      context,
      ResolveRequiredService<BearerTokenOptions>(context),
      ResolveBearerTokenLogger(context));
}