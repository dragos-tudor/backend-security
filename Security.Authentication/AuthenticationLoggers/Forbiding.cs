
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  [LoggerMessage(5, LogLevel.Information, "AuthenticationScheme: {SchemeName} was forbidden. Current path: {currentPath}. [{RequestId}]", EventName = "ForbiddenWithRedirect")]
  public static partial void LogForbidden(ILogger logger, string schemeName, string currentPath, string requestId);

}