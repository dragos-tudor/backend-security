
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static void ChallengeGoogle(HttpContext context) =>
    ChallengeGoogle(
      context,
      ResolveRequiredService<GoogleOptions>(context),
      ResolveGoogleLogger(context));
}