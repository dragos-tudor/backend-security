
using System;

namespace Security.Sample.App;

public sealed class ResponseCacheSettings
{
  public TimeSpan IntervalSeconds { get; internal set; } = TimeSpan.FromSeconds(3600);
}