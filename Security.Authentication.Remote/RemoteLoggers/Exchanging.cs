
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  [LoggerMessage(20, LogLevel.Debug, "AuthenticationScheme: {SchemeName} exchange code for tokens succedded. [{RequestId}]", EventName = "ExchangeCodeForTokens")]
  public static partial void LogExchangeCodeForTokens (ILogger logger, string schemeName, string requestId);

  [LoggerMessage(21, LogLevel.Information, "AuthenticationScheme: {SchemeName} exchange code for tokens failed. Failure message: {FailureMessage}. [{RequestId}]", EventName = "ExchangeCodeForTokensWithFailure")]
  public static partial void LogExchangeCodeForTokensWithFailure (ILogger logger, string schemeName, string failureMessage, string requestId);
}