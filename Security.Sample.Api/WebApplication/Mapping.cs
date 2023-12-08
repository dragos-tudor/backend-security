
namespace Security.Samples;

partial class Funcs {

  internal static WebApplication MapEndpoints (WebApplication app)
  {
    app.MapPost(LoginUserEndpointName, LoginUserEndpoint);
    app.MapPost(LogoutUserEndpointName, LogoutUserEndpoint);
    app.MapGet(ChallengeGoogleEndpointName, ChallengeGoogleEndpoint);
    app.MapGet(ChallengeFacebookEndpointName, ChallengeFacebookEndpoint);
    app.MapGet(ChallengeTwitterEndpointName, ChallengeTwitterEndpoint);
    return app;
  }

}