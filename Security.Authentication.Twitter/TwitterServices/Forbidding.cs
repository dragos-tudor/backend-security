
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static string? ForbidTwitter (HttpContext context, AuthenticationProperties? authProperties = default) =>
    ForbidAuth (context, ResolveRequiredService<TwitterOptions>(context), ResolveTwitterLogger(context), authProperties);
}