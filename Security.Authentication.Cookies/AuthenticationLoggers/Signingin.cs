
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  [LoggerMessage(6, LogLevel.Information, "AuthenticationScheme: {SchemeName} signed in with name id {NameId}. [{RequestId}]", EventName = "SignedInCookie")]
  public static partial void LogSignedInCookie (ILogger logger, string schemeName, string nameId, string requestId);

}