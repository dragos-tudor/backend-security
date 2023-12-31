
namespace Security.Samples;

partial class SampleFuncs {

  internal static WebApplication MapEndpoints (WebApplication app)
  {
    app.MapPost("/api/accounts/signin", SignInEndpoint);
    app.MapPost("/api/accounts/signout", SignOutEndpoint);
    app.MapFacebook(ResolveService<FacebookOptions>(app.Services), SignInCookie);
    app.MapGoogle(ResolveService<GoogleOptions>(app.Services), SignInCookie);
    app.MapTwitter(ResolveService<TwitterOptions>(app.Services), SignInCookie);
    return app;
  }

}