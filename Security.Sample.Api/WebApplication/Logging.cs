
using static System.Console;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static WebApplicationBuilder LogAppStart(WebApplicationBuilder appBuilder)
  {
    WriteLine($"[api] Server started: {appBuilder.WebHost.GetSetting("Kestrel:Endpoints:Https:Url")}");
    WriteLine($"[api] \tEnvironment: {appBuilder.Environment.EnvironmentName}");
    WriteLine($"[api] \tContent directory: {appBuilder.Environment.ContentRootPath}");
    return appBuilder;
  }
}