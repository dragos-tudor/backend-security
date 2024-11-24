
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static string? ChallengeTwitterEndpoint(HttpContext context) => ChallengeOAuth<TwitterOptions>(context, default, ResolveTwitterLogger(context));
}