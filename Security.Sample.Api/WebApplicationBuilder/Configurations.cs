
using Microsoft.Extensions.Configuration;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static IConfigurationBuilder AddConfigurations (WebApplicationBuilder app) =>
    app.Configuration.AddJsonFile("./settings.json");
}