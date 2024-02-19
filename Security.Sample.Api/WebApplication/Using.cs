
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static IApplicationBuilder UseMiddlewares (WebApplication app) =>
    app.UseDeveloperExceptionPage()     // Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
      .UseHttpsRedirection()
      .UseRouting()
      .UseAuthentication(AuthenticateCookie)
      .UseAuthorization(ChallengeGoogle, ForbidCookie);
}