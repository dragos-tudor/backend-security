using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static string? ForbidBearerToken (
    HttpContext context,
    AuthenticationProperties authProperties,
    BearerTokenOptions tokenOptions,
    ILogger logger)
  {
    SetResponseStatus(context, HttpStatusCode.Forbidden);

    LogForbidden(logger, tokenOptions.SchemeName, string.Empty, context.TraceIdentifier);
    return default;
  }

  public static string? ForbidBearerToken (
    HttpContext context,
    AuthenticationProperties authProperties,
    BearerTokenOptions tokenOptions) =>
      ForbidBearerToken(
        context,
        authProperties,
        tokenOptions,
        ResolveBearerTokenLogger(context));
}