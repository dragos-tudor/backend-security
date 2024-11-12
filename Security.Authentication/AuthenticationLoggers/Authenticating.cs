
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  [LoggerMessage(1, LogLevel.Information, "AuthenticationScheme: {SchemeName} {Message}. [{RequestId}]", EventName = "Authentication`")]
  public static partial void LogAuthentication(ILogger logger, string message, string schemeName, string requestId);

  public static AuthenticateResult LogAuthentication(ILogger logger, AuthenticateResult authResult, string schemeName, string requestId)
  {
    LogAuthentication(logger, ToAuthenticateResultString(authResult), schemeName, requestId);
    return authResult;
  }
}