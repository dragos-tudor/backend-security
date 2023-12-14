
namespace Security.Samples;

partial class SampleFuncs
{
  const string ChallengeTwitterEndpointName = "challenge-twitter";

  static string? ChallengeTwitterEndpoint(HttpContext context) =>
    ChallengeTwitter(
      context,
      CreateAuthenticationProperties(ResolveService<TimeProvider>(context).GetUtcNow())
    );
}