
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static string? ChallengeTwitter (HttpContext context, AuthenticationProperties? authProperties = default) =>
    ChallengeAuth (context, ResolveRequiredService<TwitterOptions>(context), ResolveTwitterLogger(context), authProperties);
}