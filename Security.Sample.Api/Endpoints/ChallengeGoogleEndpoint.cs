
namespace Security.Samples;

partial class SampleFuncs
{
  const string ChallengeGoogleEndpointName = "challenge-google";

  static string? ChallengeGoogleEndpoint(HttpContext context) =>
    ChallengeGoogle(
      context,
      CreateAuthenticationProperties(ResolveService<TimeProvider>(context).GetUtcNow())
    );
}