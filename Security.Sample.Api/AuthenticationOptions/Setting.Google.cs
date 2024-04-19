
using Microsoft.Extensions.Configuration;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static GoogleOptions SetGoogleOptions(ConfigurationManager configuration) =>
    CreateGoogleOptions(configuration["Secrets:Google:ClientId"]!, configuration["Secrets:Google:ClientSecret"]!) with {
      CallbackPath = $"/accounts{GoogleDefaults.CallbackPath}",
      ChallengePath = $"/accounts{GoogleDefaults.ChallengePath}"
    };
}