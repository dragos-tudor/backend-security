
using Microsoft.Extensions.Configuration;

namespace Security.Sample.Api;

partial class ApiFuncs
{
  static string[] GetCorsOrigins (ConfigurationManager configuration) => [ configuration["Cors:Origins:0"]! ];
}