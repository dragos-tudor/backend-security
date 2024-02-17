
using Microsoft.Extensions.Logging;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  [LoggerMessage(34, LogLevel.Information, "AuthenticationScheme: {SchemeName} sign out succedded. Redirect to: {RedirectUri}. [{RequestId}].", EventName = "SignedOut")]
  public static partial void LogSignedOut (ILogger logger, string schemeName, string redirectUri, string requestId);

  [LoggerMessage(32, LogLevel.Information, "AuthenticationScheme: {SchemeName} sign out failed. Failure message: {FailureMessage}. [{RequestId}]", EventName = "SignedOutWithFailure")]
  public static partial void LogSignedOutWithFailure (ILogger logger, string schemeName, string failureMessage, string requestId);

  [LoggerMessage(35, LogLevel.Information, "AuthenticationScheme: {SchemeName} is callback sign out. Redirect to: {RedirectUri}. [{RequestId}].", EventName = "SignOutCallback")]
  public static partial void LogSignOutCallback (ILogger logger, string schemeName, string redirectUri, string requestId);

  [LoggerMessage(36, LogLevel.Information, "AuthenticationScheme: {SchemeName} is remote sign out. Redirect to: {RedirectUri}. [{RequestId}].", EventName = "SignOutChallenge")]
  public static partial void LogSignOutChallenge (ILogger logger, string schemeName, string redirectUri, string requestId);
}