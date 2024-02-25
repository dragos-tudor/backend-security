
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static IApplicationBuilder UseMiddlewares (WebApplication app) =>
    app
      .UseExceptionHandler() // TODO: problem details support
      .UseHttpsRedirection()
      .UseCors()
      .UseRouting()
      .UseAuthentication(AuthenticateCookie)
      .UseAuthorization(ChallengeGoogle, ForbidCookie);
}