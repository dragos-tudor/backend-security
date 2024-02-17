
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public readonly static Func<HttpContext, string?> ChallengeGoogleEndpoint =
    AuthorizeChallengeOAuth<GoogleOptions>;
}