using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static string? ChallengeBearerToken (
    HttpContext context,
    AuthenticationProperties authProperties,
    BearerTokenOptions tokenOptions,
    ILogger logger)
  {
    SetResponseHeader(context, HeaderNames.WWWAuthenticate, "Bearer");
    SetResponseStatus(context, HttpStatusCode.Unauthorized);

    LogChallenged(logger, tokenOptions.SchemeName, string.Empty, context.TraceIdentifier);
    return default;
  }

  public static string? ChallengeBearerToken (
    HttpContext context,
    AuthenticationProperties authProperties,
    BearerTokenOptions tokenOptions) =>
      ChallengeBearerToken(
        context,
        authProperties,
        tokenOptions,
        ResolveBearerTokenLogger(context));
}