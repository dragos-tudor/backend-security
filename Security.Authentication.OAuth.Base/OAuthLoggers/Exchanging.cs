
using Microsoft.Extensions.Logging;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  [LoggerMessage(20, LogLevel.Debug, "AuthenticationScheme: {SchemeName} exchange code for tokens succedded. [{RequestId}]", EventName = "ExchangeCodeForTokens")]
  public static partial void LogExchangeCodeForTokens(ILogger logger, string schemeName, string requestId);

  [LoggerMessage(21, LogLevel.Error, "AuthenticationScheme: {SchemeName} exchange code for tokens failed. Failure message: {Failure}. [{RequestId}]", EventName = "ExchangeCodeForTokensFailure")]
  public static partial void LogExchangeCodeForTokensFailure(ILogger logger, string schemeName, string failure, string requestId);
}