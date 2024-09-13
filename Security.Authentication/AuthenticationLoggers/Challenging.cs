
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  [LoggerMessage(4, LogLevel.Information, "AuthenticationScheme: {SchemeName} was challenged. [{RequestId}].", EventName = "Challenged")]
  public static partial void LogChallenged (ILogger logger, string schemeName, string requestId);

  [LoggerMessage(4, LogLevel.Information, "AuthenticationScheme: {SchemeName} was challenged. Redirect to: {RedirectUri}. [{RequestId}].", EventName = "ChallengedWithRedirect")]
  public static partial void LogChallenged (ILogger logger, string schemeName, string redirectUri, string requestId);
}