
using Microsoft.Extensions.Configuration;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static IConfigurationBuilder AddConfigurations (WebApplicationBuilder appBuilder) =>
    appBuilder.Configuration.AddJsonFile("./settings.json");

  static string[] GetCorsOrigins (WebApplicationBuilder appBuilder) =>
    appBuilder.Configuration.GetValue<string[]>("Cors:origins") ?? [];
}