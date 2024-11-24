
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static void ChallengeFacebook(HttpContext context, FacebookOptions authOptions, ILogger logger) => ChallengeAuth(context, authOptions, logger);
}