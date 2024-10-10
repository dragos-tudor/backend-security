
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  [LoggerMessage(4, LogLevel.Information, "AuthenticationScheme: {SchemeName} was challenged. [{RequestId}].", EventName = "Challenged")]
  public static partial void LogChallenged (ILogger logger, string schemeName, string requestId);

  [LoggerMessage(4, LogLevel.Information, "AuthenticationScheme: {SchemeName} was challenged. Challenge path: {ChallengePath}. [{RequestId}].", EventName = "ChallengedWithRedirect")]
  public static partial void LogChallenged (ILogger logger, string schemeName, string challengePath, string requestId);
}