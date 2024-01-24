using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs {

  static readonly string LoggerCategory = typeof(OpenIdConnectFuncs).Namespace!;
  const string LoggerFactory = nameof(ILoggerFactory);

  static ILogger Logger {
    get {
      var contextLoggerFactory = (ILoggerFactory?)AppContext.GetData(LoggerFactory);
      var loggerFactory = contextLoggerFactory ?? NullLoggerFactory.Instance;
      return loggerFactory.CreateLogger(LoggerCategory);
    }
  }

}