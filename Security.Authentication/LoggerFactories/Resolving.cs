using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static ILogger ResolveLogger (IServiceProvider services, string categoryName) => ResolveOptionalLogger(services, categoryName) ?? CreateLogger(services, categoryName);

  public static ILogger? ResolveOptionalLogger (IServiceProvider services, string categoryName) => ResolveKeyedService<ILogger>(services, categoryName);

  public static ILoggerFactory ResolveLoggerFactory (IServiceProvider services) => ResolveOptionalLoggerFactory(services) ?? NullLoggerFactory.Instance;

  public static ILoggerFactory? ResolveOptionalLoggerFactory (IServiceProvider services) => ResolveService<ILoggerFactory>(services);

}