using Microsoft.Extensions.Logging;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  [LoggerMessage(22, LogLevel.Information, "AuthenticationScheme: {SchemeName} post authorization failed. Failure message: {FailureMessage}. [{RequestId}]", EventName = "PostAuthorizeFailure")]
  public static partial void LogPostAuthorizeFailure(ILogger logger, string schemeName, string failureMessage, string requestId);

  [LoggerMessage(23, LogLevel.Debug, "AuthenticationScheme: {SchemeName} post authorization succedded. [{RequestId}]", EventName = "PostAuthorize")]
  public static partial void LogPostAuthorize(ILogger logger, string schemeName, string requestId);
}