
using Microsoft.Extensions.Logging;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  [LoggerMessage(5, LogLevel.Information, "AuthenticationScheme: {SchemeName} was forbidden.[{RequestId}]", EventName = "Forbidden")]
  public static partial void LogForbidden (ILogger logger, string schemeName, string requestId);

  [LoggerMessage(5, LogLevel.Information, "AuthenticationScheme: {SchemeName} was forbidden. Redirect to: {RedirectUri}. [{RequestId}]", EventName = "ForbiddenWithRedirect")]
  public static partial void LogForbidden (ILogger logger, string schemeName, string redirectUri, string requestId);

}