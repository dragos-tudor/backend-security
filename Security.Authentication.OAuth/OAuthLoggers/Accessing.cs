
using Microsoft.Extensions.Logging;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  [LoggerMessage(8, LogLevel.Debug, "AuthenticationScheme: {SchemeName} accessing user informations succedded. [{RequestId}]", EventName = "AccessUserInfo")]
  public static partial void LogAccessUserInfo (ILogger logger, string schemeName, string requestId);

  [LoggerMessage(9, LogLevel.Information, "AuthenticationScheme: {SchemeName} accessing user informations failed. Failure message: {FailureMessage}. [{RequestId}]", EventName = "AccessUserInfoWithFailure")]
  public static partial void LogAccessUserInfoWithFailure (ILogger logger, string schemeName, string failureMessage, string requestId);

}