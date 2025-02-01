
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  [LoggerMessage(33, LogLevel.Information, "AuthenticationScheme: {SchemeName} callback signout. [{RequestId}].", EventName = "CallbackSigOut")]
  public static partial void LogCallbackSignOut(ILogger logger, string schemeName, string requestId);

  [LoggerMessage(34, LogLevel.Information, "AuthenticationScheme: {SchemeName} callback signout failure. Failure message: {Failure}. [{RequestId}].", EventName = "CallbackSignOutWithFailure")]
  public static partial void LogCallbackSignOutWithFailure(ILogger logger, string schemeName, string failure, string requestId);
}