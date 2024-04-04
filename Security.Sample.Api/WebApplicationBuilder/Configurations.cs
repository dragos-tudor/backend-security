
using Microsoft.Extensions.Configuration;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static IConfigurationBuilder AddCommandLine (WebApplicationBuilder appBuilder, string[] args) =>
    appBuilder.Configuration.AddCommandLine(args);

  static IConfigurationBuilder AddEnvironmentVariables (WebApplicationBuilder appBuilder) =>
    appBuilder.Configuration.AddEnvironmentVariables();

  static IConfigurationBuilder AddSecrets (WebApplicationBuilder app) =>
    app.Configuration
      .AddJsonFile("./secrets.json")
      .AddUserSecrets(typeof(SampleFuncs).Assembly, true);

  static IConfigurationBuilder AddSettings (WebApplicationBuilder appBuilder) =>
    appBuilder.Configuration
      .AddJsonFile("./settings.json");
}