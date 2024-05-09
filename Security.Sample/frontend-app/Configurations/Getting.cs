
using System;
using Microsoft.Extensions.Configuration;

namespace Security.Sample.App;

partial class AppFuncs
{
  static readonly TimeSpan DefaultCacheInterval = TimeSpan.FromHours(24);

  static ResponseCacheOptions? GetResponseCacheOptions (ConfigurationManager configuration) =>
    configuration.GetSection(nameof(ResponseCacheOptions)).Get<ResponseCacheOptions>();

  static TimeSpan GetResponseCacheInterval (ConfigurationManager configuration) =>
    GetResponseCacheOptions(configuration)?.IntervalSeconds ?? DefaultCacheInterval;
}