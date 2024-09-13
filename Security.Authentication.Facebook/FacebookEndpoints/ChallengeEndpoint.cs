
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static string? ChallengeFacebookEndpoint (HttpContext context) => ChallengeOAuth<FacebookOptions>(context, ResolveFacebookLogger(context));
}