
using Microsoft.Extensions.Logging;

namespace Security.Authentication;

partial class Funcs {

  [LoggerMessage(5, LogLevel.Information, "AuthenticationScheme: {SchemeName} was forbidden. Redirect to: {RedirectUri}. [{RequestId}]", EventName = "Forbidden")]
  public static partial void LogForbidden (ILogger logger, string schemeName, string redirectUri, string requestId);

}