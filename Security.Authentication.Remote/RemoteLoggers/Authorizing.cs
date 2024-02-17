
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  [LoggerMessage(27, LogLevel.Information, "AuthenticationScheme: {SchemeName} is authorizing remote. Redirect to: {RedirectUri}. [{RequestId}].", EventName = "AuthorizeRemote")]
  public static partial void LogAuthorizeRemote (ILogger logger, string schemeName, string redirectUri, string requestId);

}