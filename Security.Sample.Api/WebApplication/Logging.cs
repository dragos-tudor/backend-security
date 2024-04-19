
using static System.Console;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static WebApplicationBuilder LogAppStart(WebApplicationBuilder builder)
  {
    WriteLine($"[api] Server started: {builder.WebHost.GetSetting("Kestrel:Endpoints:Https:Url")}");
    WriteLine($"[api] \tEnvironment: {builder.Environment.EnvironmentName}");
    WriteLine($"[api] \tContent directory: {builder.Environment.ContentRootPath}");
    return builder;
  }
}