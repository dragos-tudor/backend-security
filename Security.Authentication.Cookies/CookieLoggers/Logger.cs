using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static readonly string LoggerCategory = typeof(CookiesFuncs).Namespace!;
  const string LoggerFactory = nameof(ILoggerFactory);

  static ILogger Logger {
    get {
      var contextLoggerFactory = (ILoggerFactory?)AppContext.GetData(LoggerFactory);
      var loggerFactory = contextLoggerFactory ?? NullLoggerFactory.Instance;
      return loggerFactory.CreateLogger(LoggerCategory);
    }
  }

}