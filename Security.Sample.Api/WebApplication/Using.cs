
namespace Security.Samples;

partial class Funcs {

  internal static WebApplication UseMiddlewares (WebApplication app)
  {
    app.UseDeveloperExceptionPage();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseRemoteAuthentication(AuthenticateRemoteAsync);
    app.UseLocalAuthentication(AuthenticateLocal);
    app.UseSchemeAuthorization(AuthenticateScheme, ChallengeScheme!, ForbidScheme!);
    return app;
  }
}