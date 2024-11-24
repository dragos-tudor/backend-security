
using Microsoft.Extensions.Logging;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  [LoggerMessage(27, LogLevel.Information, "AuthenticationScheme: {SchemeName} is authorizing challenge. Location: {location}. SetCookie: {SetCookie}, [{RequestId}].", EventName = "AuthorizeChallenge")]
  public static partial void LogAuthorizeChallenge(ILogger logger, string schemeName, string location, string setCookie, string requestId);
}