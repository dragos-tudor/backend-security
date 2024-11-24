
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static void ChallengeTwitter(HttpContext context, TwitterOptions authOptions, ILogger logger) => ChallengeAuth(context, authOptions, logger);
}