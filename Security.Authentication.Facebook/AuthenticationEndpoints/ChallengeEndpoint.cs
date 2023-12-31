
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public readonly static Func<HttpContext, string?> ChallengeFacebookEndpoint =
    ChallengeOAuth<FacebookOptions>;
}