
using Microsoft.Extensions.Configuration;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static IConfigurationBuilder AddSecrets (WebApplicationBuilder app, string[] args) =>
    app.Configuration
      .AddJsonFile("./secrets.json")
      .AddEnvironmentVariables()
      .AddCommandLine(args)
      .AddUserSecrets(typeof(SampleFuncs).Assembly, true);
}