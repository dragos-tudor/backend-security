
using Microsoft.Extensions.Configuration;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static TwitterOptions SetTwitterOptions(ConfigurationManager configuration) =>
    CreateTwitterOptions(configuration["Secrets:Twitter:ConsumerKey"]!, configuration["Secrets:Twitter:ConsumerSecret"]!) with {
      CallbackPath = $"/accounts{TwitterDefaults.CallbackPath}",
      ChallengePath = $"/accounts{TwitterDefaults.ChallengePath}"
    };
}