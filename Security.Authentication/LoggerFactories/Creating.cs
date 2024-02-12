using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static ILogger CreateLogger(ILoggerFactory? loggerFactory, string loggerCategory) =>
    (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger(loggerCategory);
}