
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
      .UseAuthorization(
        AuthenticateScheme,
        (context, _) => ChallengeCookie(context),
        (context, _) => ForbidCookie(context));
    return app;
  }
}