
using Microsoft.Extensions.Configuration;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static string[] GetCorsOrigins (WebApplicationBuilder appBuilder) =>
    appBuilder.Configuration.GetValue<string[]>("Cors:origins") ?? [];
}