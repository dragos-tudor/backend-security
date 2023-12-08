
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Remote;

partial class Funcs {

  [LoggerMessage(25, LogLevel.Information, "AuthenticationScheme: {SchemeName} An error was encountered while handling the remote login. Failure message: {FailureMessage}", EventName = "AuthenticateRemoteFailure")]
  public static partial void AuthenticateRemoteFailureLog (ILogger logger, string schemeName, string failureMessage);

  [LoggerMessage(26, LogLevel.Information, "AuthenticationScheme: {SchemeName} Handling remote login succedded.", EventName = "AuthenticateRemote")]
  public static partial void AuthenticateRemoteLog (ILogger logger, string schemeName);

}