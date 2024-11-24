
using Microsoft.Extensions.Logging;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  [LoggerMessage(28, LogLevel.Debug, "AuthenticationScheme: {SchemeName} accessing user informations succedded. [{RequestId}]", EventName = "AccessUserInfo")]
  public static partial void LogAccessUserInfo(ILogger logger, string schemeName, string requestId);

  [LoggerMessage(29, LogLevel.Information, "AuthenticationScheme: {SchemeName} accessing user informations failed. Failure message: {FailureMessage}. [{RequestId}]", EventName = "AccessUserInfoFailure")]
  public static partial void LogAccessUserInfoFailure(ILogger logger, string schemeName, string failureMessage, string requestId);
}