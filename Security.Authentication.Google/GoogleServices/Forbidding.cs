
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static string ForbidGoogle (HttpContext context, AuthenticationProperties authProperties) =>
    ForbidAuth (context, authProperties, ResolveRequiredService<GoogleOptions>(context), ResolveGoogleLogger(context));
}