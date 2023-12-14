
using Microsoft.Extensions.Configuration;

namespace Security.Samples;

partial class SampleFuncs {

  internal static IConfigurationBuilder AddConfigurationProviders (WebApplicationBuilder app) =>
    app.Configuration.AddJsonFile("./settings.json");

}