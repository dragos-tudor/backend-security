
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static string? ChallengeFacebook (HttpContext context, AuthenticationProperties? authProperties = default) =>
    ChallengeAuth (context, ResolveRequiredService<FacebookOptions>(context), ResolveFacebookLogger(context), authProperties);
}