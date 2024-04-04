using Microsoft.Extensions.Logging;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  const string DefaultLoggerFactoryKey = nameof(ILoggerFactory);

  public static void SetLoggerFactory(ILoggerFactory loggerFactory, string? factoryKey = default) =>
    AppContext.SetData(factoryKey ?? DefaultLoggerFactoryKey, loggerFactory);
}