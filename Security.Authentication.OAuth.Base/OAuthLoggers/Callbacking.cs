
using Microsoft.Extensions.Logging;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  [LoggerMessage(31, LogLevel.Information, "AuthenticationScheme: {SchemeName} remote challenge. [{RequestId}].", EventName = "CallbackOAuth")]
  public static partial void LogCallbackOAuth(ILogger logger, string schemeName, string requestId);

  [LoggerMessage(32, LogLevel.Information, "AuthenticationScheme: {SchemeName} remote challenge failure. Failure message: {Failure}. [{RequestId}].", EventName = "CallbackOAuthWithFailure")]
  public static partial void LogCallbackOAuthWithFailure(ILogger logger, string schemeName, string failure, string requestId);
}