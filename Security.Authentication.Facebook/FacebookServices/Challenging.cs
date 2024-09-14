
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static string? ChallengeFacebook (HttpContext context) =>
    ChallengeAuth (context, ResolveRequiredService<FacebookOptions>(context), ResolveFacebookLogger(context));
}