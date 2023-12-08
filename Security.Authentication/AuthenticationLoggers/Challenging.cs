
using Microsoft.Extensions.Logging;

namespace Security.Authentication;

partial class Funcs {

  [LoggerMessage(4, LogLevel.Information, "AuthenticationScheme: {SchemeName} was challenged. Redirect to: {RedirectUri}. [{RequestId}].", EventName = "Challenged")]
  public static partial void LogChallenged (ILogger logger, string schemeName, string redirectUri, string requestId);

}