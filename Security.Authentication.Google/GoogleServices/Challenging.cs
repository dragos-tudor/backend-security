
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static string ChallengeGoogle (HttpContext context, AuthenticationProperties authProperties) =>
    ChallengeAuth (context, authProperties, ResolveRequiredService<GoogleOptions>(context), ResolveGoogleLogger(context));
}