
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static WebApplication MapEndpoints (WebApplication app)
  {
    app.MapPost("/api/accounts/signin", (Delegate)SignInEndpoint);
    app.MapPost("/api/accounts/signout", SignOutEndpoint);
    app.MapFacebook(ResolveService<FacebookOptions>(app.Services), SignInCookie);
    app.MapGoogle(ResolveService<GoogleOptions>(app.Services), SignInCookie);
    app.MapTwitter(ResolveService<TwitterOptions>(app.Services), SignInCookie);
    return app;
  }
}