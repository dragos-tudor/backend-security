
namespace Security.Sample.Api;

partial class ApiFuncs
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