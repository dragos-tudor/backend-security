
using static System.Console;

namespace Security.Sample.Api;

partial class ApiFuncs
{
  static WebApplication LogApplicationStart(WebApplication app)
  {
    WriteLine($"[backend-api] Server started: {app.Configuration["Kestrel:Endpoints:Https:Url"]}");
    WriteLine($"[backend-api] \tEnvironment: {app.Environment.EnvironmentName}");
    return app;
  }
}