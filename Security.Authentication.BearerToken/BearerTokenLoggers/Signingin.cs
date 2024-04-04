
using Microsoft.Extensions.Logging;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs {

  [LoggerMessage(21, LogLevel.Information, "AuthenticationScheme: {SchemeName} signed in with name id {NameId}. [{RequestId}]", EventName = "SignedInBearerToken")]
  public static partial void LogSignInBearerToken (ILogger logger, string schemeName, string nameId, string requestId);

}