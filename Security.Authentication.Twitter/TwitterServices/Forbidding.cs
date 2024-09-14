
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static string? ForbidTwitter (HttpContext context) =>
    ForbidAuth (context, ResolveRequiredService<TwitterOptions>(context), ResolveTwitterLogger(context));
}