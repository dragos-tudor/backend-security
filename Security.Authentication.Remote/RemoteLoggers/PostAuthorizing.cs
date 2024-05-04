using Microsoft.Extensions.Logging;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  [LoggerMessage(22, LogLevel.Information, "AuthenticationScheme: {SchemeName} post authorization failed. Failure message: {FailureMessage}. [{RequestId}]", EventName = "PostAuthorizationFailure")]
  public static partial void LogPostAuthorizationFailure (ILogger logger, string schemeName, string failureMessage, string requestId);

  [LoggerMessage(23, LogLevel.Debug, "AuthenticationScheme: {SchemeName} post authorization succedded. [{RequestId}]", EventName = "PostAuthorization")]
  public static partial void LogPostAuthorization (ILogger logger, string schemeName, string requestId);
}