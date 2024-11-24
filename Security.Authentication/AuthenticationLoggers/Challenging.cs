
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  [LoggerMessage(4, LogLevel.Information, "AuthenticationScheme: {SchemeName} was challenged. Current path: {currentPath}. [{RequestId}].", EventName = "ChallengedWithRedirect")]
  public static partial void LogChallenged(ILogger logger, string schemeName, string currentPath, string requestId);
}