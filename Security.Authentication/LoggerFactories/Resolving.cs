using Microsoft.Extensions.Logging.Abstractions;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static ILoggerFactory ResolveLoggerFactory(IServiceProvider services) => ResolveService<ILoggerFactory>(services) ?? NullLoggerFactory.Instance;

  public static ILogger ResolveLogger(IServiceProvider services, string categoryName) => ResolveKeyedService<ILogger>(services, categoryName) ?? CreateLogger(services, categoryName);
}