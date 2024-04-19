
using static System.Console;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static WebApplication LogApplicationStart(WebApplication app)
  {
    WriteLine($"[api] Server started: {app.Configuration["Kestrel:Endpoints:Https:Url"]}");
    WriteLine($"[api] \tEnvironment: {app.Environment.EnvironmentName}");
    WriteLine($"[api] \tContent directory: {app.Environment.ContentRootPath}");
    return app;
  }
}