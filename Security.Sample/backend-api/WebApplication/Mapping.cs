
namespace Security.Sample.Api;

partial class ApiFuncs
{
  static WebApplication MapEndpoints (WebApplication app)
  {
    app.MapPost("/accounts/signin", SignInAccount);
    app.MapPost("/accounts/signout", SignOutAccount).RequireAuthorization();
    app.MapGet("/accounts/authenticated", IsAuthenticatedAccount);
    app.MapGet("/users", GetUserInfo).RequireAuthorization();

    app.MapFacebook(ResolveService<FacebookOptions>(app.Services), SignInCookie);
    app.MapGoogle(ResolveService<GoogleOptions>(app.Services), SignInCookie);
    app.MapTwitter(ResolveService<TwitterOptions>(app.Services), SignInCookie);
    return app;
  }
}