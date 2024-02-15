
using Microsoft.Extensions.Logging;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs {

  [LoggerMessage(38, LogLevel.Debug, "AuthenticationScheme: {SchemeName} accessing user informations succedded. [{RequestId}]", EventName = "AccessUserInfo")]
  public static partial void LogAccessUserInfo (ILogger logger, string schemeName, string requestId);

  [LoggerMessage(39, LogLevel.Information, "AuthenticationScheme: {SchemeName} accessing user informations failed. Failure message: {FailureMessage}. [{RequestId}]", EventName = "AccessUserInfoWithFailure")]
  public static partial void LogAccessUserInfoWithFailure (ILogger logger, string schemeName, string failureMessage, string requestId);

}