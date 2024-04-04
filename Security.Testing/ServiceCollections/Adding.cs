using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Time.Testing;

namespace Security.Testing;

partial class Funcs
{
  public static IServiceCollection AddTestServices(
    this IServiceCollection services,
    LogLevel minimumLogLevel = LogLevel.None) =>
      services
        .AddLogging(o => o.SetMinimumLevel(minimumLogLevel))
        .AddDataProtection($"{Environment.CurrentDirectory}/keys")
        .AddSingleton<TimeProvider>(new FakeTimeProvider());
}