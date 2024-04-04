
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public readonly static Func<HttpContext, string?> ChallengeTwitterEndpoint =
    AuthorizeChallengeOAuth<TwitterOptions>;
}