using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static readonly string LoggerCategory = typeof(AuthorizationFuncs).Namespace!;
  const string LoggerFactory = nameof(ILoggerFactory);

  static ILogger Logger {
    get {
      var contextLoggerFactory = (ILoggerFactory?)AppContext.GetData(LoggerFactory);
      var loggerFactory = contextLoggerFactory ?? NullLoggerFactory.Instance;
      return loggerFactory.CreateLogger(LoggerCategory);
    }
  }

}