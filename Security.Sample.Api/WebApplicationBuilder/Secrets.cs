
using Microsoft.Extensions.Configuration;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static IConfigurationBuilder AddSecrets (WebApplicationBuilder app) =>
    app.Configuration.AddJsonFile("./secrets.json");
}