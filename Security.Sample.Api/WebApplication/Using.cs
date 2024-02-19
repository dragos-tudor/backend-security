
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static IApplicationBuilder UseMiddlewares (WebApplication app) =>
    app.UseDeveloperExceptionPage()
      .UseHttpsRedirection()
      .UseStaticFiles()
      .UseRouting()
      .UseAuthentication(AuthenticateCookie)
      .UseAuthorization(ChallengeGoogle, ForbidCookie);
}