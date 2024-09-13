
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static string ForbidFacebook (HttpContext context, AuthenticationProperties authProperties) =>
    ForbidAuth (context, authProperties, ResolveRequiredService<FacebookOptions>(context), ResolveFacebookLogger(context));
}