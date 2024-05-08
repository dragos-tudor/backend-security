
using static System.Console;

namespace Security.Sample.App;

partial class AppFuncs
{
  static WebApplication LogApplicationStart(WebApplication app)
  {
    WriteLine($"[frontend-app] Server started: {app.Configuration["Kestrel:Endpoints:Https:Url"]}");
    WriteLine($"[frontend-app] \tEnvironment: {app.Environment.EnvironmentName}");
    WriteLine($"[frontend-app] \tWeb root: {app.Environment.WebRootPath}");
    return app;
  }
}