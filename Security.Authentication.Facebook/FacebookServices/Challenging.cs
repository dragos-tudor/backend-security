
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static string ChallengeFacebook (HttpContext context, AuthenticationProperties authProperties) =>
    ChallengeAuth (context, authProperties, ResolveRequiredService<FacebookOptions>(context), ResolveFacebookLogger(context));
}