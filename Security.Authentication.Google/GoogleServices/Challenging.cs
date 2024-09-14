
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static string? ChallengeGoogle (HttpContext context) =>
    ChallengeAuth (context, ResolveRequiredService<GoogleOptions>(context), ResolveGoogleLogger(context));
}