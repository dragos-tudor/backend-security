
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  [LoggerMessage(7, LogLevel.Information, "AuthenticationScheme: {SchemeName} signed out {NameId}. [{RequestId}]", EventName = "SignedOutCookie")]
  public static partial void LogSignedOutCookie(ILogger logger, string schemeName, string nameId, string requestId);

}