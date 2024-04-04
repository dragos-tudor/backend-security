
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static WebApplication MapEndpoints (WebApplication app)
  {
    app.MapPost("/accounts/signin", SignInEndpoint);
    app.MapPost("/accounts/signout", SignOutEndpoint).RequireAuthorization();
    app.MapGet("/users", GetUserInfoEndpoint).RequireAuthorization();

    app.MapFacebook(ResolveService<FacebookOptions>(app.Services), SignInCookie);
    app.MapGoogle(ResolveService<GoogleOptions>(app.Services), SignInCookie);
    app.MapTwitter(ResolveService<TwitterOptions>(app.Services), SignInCookie);
    return app;
  }
}