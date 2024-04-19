
using Microsoft.Extensions.Configuration;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static string[] GetCorsOrigins (ConfigurationManager configuration) => [ configuration["Cors:Origins:0"]! ];
}