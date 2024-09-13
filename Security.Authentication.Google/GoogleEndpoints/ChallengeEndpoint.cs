
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static string? ChallengeGoogleEndpoint (HttpContext context) => ChallengeOAuth<GoogleOptions>(context, ResolveGoogleLogger(context));
}