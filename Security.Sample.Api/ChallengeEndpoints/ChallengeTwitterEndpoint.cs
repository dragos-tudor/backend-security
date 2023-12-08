
namespace Security.Samples;

partial class Funcs {

  const string ChallengeTwitterEndpointName = "challenge-twitter";

  readonly static Func<HttpContext, string?> ChallengeTwitterEndpoint = (context) =>
    ChallengeTwitter(
      context,
      CreateAuthenticationProperties(ResolveService<TimeProvider>(context).GetUtcNow()),
      ResolveService<TwitterOptions>(context),
      ResolveService<TimeProvider>(context).GetUtcNow()
    );

}