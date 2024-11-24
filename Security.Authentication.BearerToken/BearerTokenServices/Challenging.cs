using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static void ChallengeBearerToken(
    HttpContext context,
    BearerTokenOptions authOptions,
    ILogger logger)
  {
    SetHttpResponseHeader(context, HeaderNames.WWWAuthenticate, BearerName);
    ChallengeAuth(context, authOptions, logger);
  }
}