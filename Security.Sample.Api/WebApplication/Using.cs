
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static IApplicationBuilder UseMiddlewares (WebApplication app) =>
    app
      .UseExceptionHandler()
      .UseRouting()
      .UseCors()
      .UseAuthentication(AuthenticateCookie)
      .UseAuthorization(
        ChallengeAuth<CookieAuthenticationOptions>,
        ForbidAuth<CookieAuthenticationOptions>);
}