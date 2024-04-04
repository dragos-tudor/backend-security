using Microsoft.Extensions.Logging;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  [LoggerMessage(22, LogLevel.Information, "AuthenticationScheme: {SchemeName} post authorization failed. Failure message: {FailureMessage}. [{RequestId}]", EventName = "PostAuthorizationWithFailure")]
  public static partial void LogPostAuthorizationWithFailure (ILogger logger, string schemeName, string failureMessage, string requestId);

  [LoggerMessage(23, LogLevel.Debug, "AuthenticationScheme: {SchemeName} post authorization succedded. [{RequestId}]", EventName = "PostAuthorization")]
  public static partial void LogPostAuthorization (ILogger logger, string schemeName, string requestId);
}