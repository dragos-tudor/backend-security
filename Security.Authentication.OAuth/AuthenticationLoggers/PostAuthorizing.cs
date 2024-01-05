using Microsoft.Extensions.Logging;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  [LoggerMessage(12, LogLevel.Information, "AuthenticationScheme: {SchemeName} post authorizing failed. Failure message: {FailureMessage}. [{RequestId}]", EventName = "PostAuthorizeWithFailure")]
  public static partial void LogPostAuthorizeWithFailure (ILogger logger, string schemeName, string failureMessage, string requestId);

  [LoggerMessage(13, LogLevel.Debug, "AuthenticationScheme: {SchemeName} post authorizing succedded. [{RequestId}]", EventName = "PostAuthorized")]
  public static partial void LogPostAuthorize (ILogger logger, string schemeName, string requestId);

}