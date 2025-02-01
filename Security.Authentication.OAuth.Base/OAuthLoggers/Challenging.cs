
using Microsoft.Extensions.Logging;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  [LoggerMessage(30, LogLevel.Information, "AuthenticationScheme: {SchemeName} authorization challenge. Location: {location}. SetCookie: {SetCookie}, [{RequestId}].", EventName = "ChallengeOAuth")]
  public static partial void LogChallengeOAuth(ILogger logger, string schemeName, string location, string setCookie, string requestId);
}