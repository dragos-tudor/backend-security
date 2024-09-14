
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static string? ForbidFacebook (HttpContext context) =>
    ForbidAuth (context, ResolveRequiredService<FacebookOptions>(context), ResolveFacebookLogger(context));
}