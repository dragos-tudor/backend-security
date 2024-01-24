
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  [LoggerMessage(27, LogLevel.Information, "AuthenticationScheme: {SchemeName} was challenged remote. Redirect to: {RedirectUri}. [{RequestId}].", EventName = "Challenged")]
  public static partial void LogChallengedRemote (ILogger logger, string schemeName, string redirectUri, string requestId);

}