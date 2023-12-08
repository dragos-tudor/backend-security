
namespace Security.Samples;

partial class Funcs {

  const string ChallengeGoogleEndpointName = "challenge-google";

  readonly static Func<HttpContext, string?> ChallengeGoogleEndpoint = (context) =>
    ChallengeGoogle(
      context,
      CreateAuthenticationProperties(ResolveService<TimeProvider>(context).GetUtcNow()),
      ResolveService<GoogleOptions>(context),
      ResolveService<TimeProvider>(context).GetUtcNow()
    );

}