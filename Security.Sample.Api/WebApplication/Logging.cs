
using static System.Console;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static WebApplicationBuilder LogAppStart(WebApplicationBuilder appBuilder)
  {
    WriteLine($"Server started: {appBuilder.WebHost.GetSetting("Kestrel:Endpoints:Https:Url")}");
    WriteLine($"  Environment: {appBuilder.Environment.EnvironmentName}");
    WriteLine($"  Content directory: {appBuilder.Environment.ContentRootPath}");
    return appBuilder;
  }
}