using Microsoft.Extensions.Logging;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static ILoggerFactory? GetLoggerFactory(string? factoryKey = default) =>
    (ILoggerFactory?)AppContext.GetData(factoryKey ?? DefaultLoggerFactoryKey);
}