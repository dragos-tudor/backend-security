
namespace Security.Sample.Api;

partial class SampleFuncs
{
  static string[] GetCorsOrigins (WebApplicationBuilder appBuilder) =>
    [ appBuilder.Configuration["Cors:origins:0"]! ];
}