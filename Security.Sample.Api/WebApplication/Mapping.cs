
namespace Security.Samples;

partial class SampleFuncs {

  internal static WebApplication MapEndpoints (WebApplication app)
  {
    app.MapPost(LoginUserEndpointName, LoginUserEndpoint);
    app.MapPost(LogoutUserEndpointName, LogoutUserEndpoint);

    app.MapGet(ChallengeGoogleEndpointName, ChallengeGoogleEndpoint);
    app.MapGet(ChallengeFacebookEndpointName, ChallengeFacebookEndpoint);
    app.MapGet(ChallengeTwitterEndpointName, ChallengeTwitterEndpoint);

    app.MapGet(ResolveService<GoogleOptions>(app.Services).CallbackPath, (Delegate)CallbackGoogleEndpoint);
    app.MapGet(ResolveService<FacebookOptions>(app.Services).CallbackPath, (Delegate)CallbackFacebookEndpoint);
    app.MapGet(ResolveService<TwitterOptions>(app.Services).CallbackPath, (Delegate)CallbackTwitterEndpoint);

    return app;
  }

}