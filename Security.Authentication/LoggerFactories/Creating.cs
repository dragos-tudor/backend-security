
using Microsoft.Extensions.Logging;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static ILogger CreateLogger (IServiceProvider services, string categoryName) => ResolveLoggerFactory(services).CreateLogger(categoryName);
}