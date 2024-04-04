
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  [LoggerMessage(1, LogLevel.Information, "AuthenticationScheme: {SchemeName} was not authenticated. Failure message: {FailureMessage}. [{RequestId}]", EventName = "NotAuthenticatedWithFailure")]
  public static partial void LogNotAuthenticatedWithFailure (ILogger logger, string schemeName, string failureMessage, string requestId);

  [LoggerMessage(2, LogLevel.Debug, "AuthenticationScheme: {SchemeName} was successfully authenticated. Name identifier: {NameId}. [{RequestId}]", EventName = "Authenticated")]
  public static partial void LogAuthenticated (ILogger logger, string schemeName, string nameId, string requestId);

  [LoggerMessage(3, LogLevel.Debug, "AuthenticationScheme: {SchemeName} was not authenticated. [{RequestId}]", EventName = "NotAuthenticated")]
  public static partial void LogNotAuthenticated (ILogger logger, string schemeName, string requestId);

  public static AuthenticateResult LogAuthenticationResult(
    ILogger logger,
    AuthenticateResult autResult,
    string schemeName,
    string traceId)
  {
    if(autResult.None) LogNotAuthenticated(logger, schemeName, traceId);
    if(autResult.Failure is not null) LogNotAuthenticatedWithFailure(logger, schemeName, autResult.Failure.Message, traceId);
    if(autResult.Succeeded) LogAuthenticated(logger, schemeName, GetPrincipalNameId(autResult.Principal)!, traceId);
    return autResult;
  }
}