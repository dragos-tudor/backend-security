
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static IApplicationBuilder UseMiddlewares (WebApplication app) =>
    app
      .UseHttpsRedirection()
      .UseExceptionHandler()
      .UseCors()
      .UseRouting()
      .UseAuthentication(AuthenticateCookie)
      .UseAuthorization(
        ChallengeAuth<CookieAuthenticationOptions>,
        ForbidAuth<CookieAuthenticationOptions>);
}