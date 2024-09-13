
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static string ForbidTwitter (HttpContext context, AuthenticationProperties authProperties) =>
    ForbidAuth (context, authProperties, ResolveRequiredService<TwitterOptions>(context), ResolveTwitterLogger(context));
}