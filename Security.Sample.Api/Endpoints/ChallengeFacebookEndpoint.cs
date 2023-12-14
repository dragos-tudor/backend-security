
namespace Security.Samples;

partial class SampleFuncs
{
  const string ChallengeFacebookEndpointName = "challenge-facebook";

  static string? ChallengeFacebookEndpoint(HttpContext context) =>
    ChallengeFacebook(
      context,
      CreateAuthenticationProperties(ResolveService<TimeProvider>(context).GetUtcNow())
    );
}