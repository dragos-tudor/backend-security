
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  [LoggerMessage(27, LogLevel.Information, "AuthenticationScheme: {SchemeName} is authorizing challenge. Redirect to: {RedirectUri}. [{RequestId}].", EventName = "AuthorizeChallenge")]
  public static partial void LogAuthorizeChallenge (ILogger logger, string schemeName, string redirectUri, string requestId);
}