
namespace Security.Samples;

partial class Funcs {

  const string ChallengeFacebookEndpointName = "challenge-facebook";

  readonly static Func<HttpContext, string?> ChallengeFacebookEndpoint = (context) =>
    ChallengeFacebook(
      context,
      CreateAuthenticationProperties(ResolveService<TimeProvider>(context).GetUtcNow()),
      ResolveService<FacebookOptions>(context),
      ResolveService<TimeProvider>(context).GetUtcNow()
    );

}