
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  [LoggerMessage(35, LogLevel.Information, "AuthenticationScheme: {SchemeName} authorization challenge signout. Location: {location}. SetCookie: {SetCookie}, [{RequestId}].", EventName = "LogChallengeSignOut")]
  public static partial void LogChallengeSignOut(ILogger logger, string schemeName, string location, string setCookie, string requestId);

  [LoggerMessage(36, LogLevel.Error, "AuthenticationScheme: {SchemeName} authorization challenge signout failure. Failure message: {Failure}, [{RequestId}].", EventName = "ChallengeSignOutWithFailure")]
  public static partial void LogChallengeSignOutWithFailure(ILogger logger, string schemeName, string failure, string requestId);
}