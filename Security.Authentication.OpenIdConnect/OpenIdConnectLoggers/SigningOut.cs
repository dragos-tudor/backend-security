
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  [LoggerMessage(31, LogLevel.Information, "AuthenticationScheme: {SchemeName} sign out succedded. Redirect to: {RedirectUri}. [{RequestId}].", EventName = "SignedOut")]
  public static partial void LogSignedOut(ILogger logger, string schemeName, string redirectUri, string requestId);

  [LoggerMessage(32, LogLevel.Error, "AuthenticationScheme: {SchemeName} sign out failed. Failure message: {FailureMessage}. [{RequestId}]", EventName = "SignedOutWithFailure")]
  public static partial void LogSignedOutWithFailure(ILogger logger, string schemeName, string failureMessage, string requestId);

  [LoggerMessage(34, LogLevel.Information, "AuthenticationScheme: {SchemeName} remote sign out. Redirect to: {RedirectUri}. [{RequestId}].", EventName = "SignOutChallenge")]
  public static partial void LogSignOutChallenge(ILogger logger, string schemeName, string redirectUri, string requestId);

  [LoggerMessage(35, LogLevel.Information, "AuthenticationScheme: {SchemeName} skip remote sign out. {Message}. [{RequestId}].", EventName = "SignSkipOutChallenge")]
  public static partial void LogSkipSignOutChallenge(ILogger logger, string schemeName, string message, string requestId);
}