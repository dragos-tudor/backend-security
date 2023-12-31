
namespace Security.Samples;

partial class SampleFuncs
{
  internal static WebApplication UseMiddlewares (WebApplication app)
  {
    app.UseDeveloperExceptionPage()
      .UseHttpsRedirection()
      .UseStaticFiles()
      .UseRouting()
      .UseAuthentication(AuthenticateCookie)
      .UseAuthorization(ChallengeGoogle, ForbidCookie);
    return app;
  }
}