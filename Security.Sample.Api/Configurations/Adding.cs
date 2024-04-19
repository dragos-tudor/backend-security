
using Microsoft.Extensions.Configuration;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static IConfigurationBuilder AddCommandLine (ConfigurationManager configuration, string[] args) =>
    configuration.AddCommandLine(args);

  static IConfigurationBuilder AddEnvironmentVariables (ConfigurationManager configuration) =>
    configuration.AddEnvironmentVariables();

  static IConfigurationBuilder AddSecrets (ConfigurationManager configuration) =>
    configuration.AddJsonFile("./secrets.json").AddUserSecrets(typeof(SampleFuncs).Assembly, true);

  static IConfigurationBuilder AddSettings (ConfigurationManager configuration) =>
    configuration.AddJsonFile("./settings.json");
}