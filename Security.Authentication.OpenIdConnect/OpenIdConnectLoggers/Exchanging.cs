
using Microsoft.Extensions.Logging;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  [LoggerMessage(30, LogLevel.Debug, "AuthenticationScheme: {SchemeName} exchange code for tokens succedded. [{RequestId}]", EventName = "ExchangeCodeForTokens")]
  public static partial void LogExchangeCodeForTokens (ILogger logger, string schemeName, string requestId);

  [LoggerMessage(31, LogLevel.Information, "AuthenticationScheme: {SchemeName} exchange code for tokens failed. Failure message: {FailureMessage}. [{RequestId}]", EventName = "ExchangeCodeForTokensWithFailure")]
  public static partial void LogExchangeCodeForTokensWithFailure (ILogger logger, string schemeName, string failureMessage, string requestId);
}