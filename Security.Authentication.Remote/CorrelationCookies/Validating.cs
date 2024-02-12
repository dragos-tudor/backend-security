
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  internal const string CorrelationCookieNotFound = "Correlation cookie not found";
  internal const string UnexpectedCorrelationCookieContent = "Unexpected correlation cookie";
  internal const string CorrelationIdKeyNotFound = "Correlation id not found on authentication properties";
  internal const string CorrelationFailed = "Correlation failed";

  internal static string? ValidateCorrelationCookie (HttpRequest request, string correlationId)
  {
    var cookieName = GetCorrelationCookieName(correlationId);
    var cookieContent = GetCorrelationCookieContent(request, cookieName);

    if (IsEmptyCorrelationContent(cookieContent)) return CorrelationCookieNotFound;
    if (!IsCorrelationContentMatch(cookieContent!)) return UnexpectedCorrelationCookieContent;
    return default;
  }

  public static string? ValidateCorrelationCookie (HttpRequest request, AuthenticationProperties authProperties) {
    if (GetAuthenticationPropertiesCorrelationId(authProperties!) is not string correlationId) return CorrelationIdKeyNotFound;
    if (ValidateCorrelationCookie(request, correlationId) is string correlationError) return $"{CorrelationFailed} {correlationError}";
    return default;
  }
}