
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static string ChallengeTwitter (HttpContext context, AuthenticationProperties authProperties) =>
    ChallengeAuth (context, authProperties, ResolveRequiredService<TwitterOptions>(context), ResolveTwitterLogger(context));
}