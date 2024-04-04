using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static string? ChallengeBearerToken(
    HttpContext context,
    AuthenticationProperties authProperties,
    BearerTokenOptions tokenOptions)
  {
    SetResponseHeader(context, HeaderNames.WWWAuthenticate, "Bearer");
    SetResponseStatus(context, HttpStatusCode.Unauthorized);

    LogChallenged(Logger, tokenOptions.SchemeName, string.Empty, context.TraceIdentifier);
    return default;
  }
}