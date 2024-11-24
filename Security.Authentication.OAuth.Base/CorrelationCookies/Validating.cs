
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs {

  internal const string CorrelationCookieNotFound = "correlation cookie not found";
  internal const string UnexpectedCorrelationCookieContent = "unexpected correlation cookie";
  internal const string CorrelationIdKeyNotFound = "correlation id not found on authentication properties";
  internal const string CorrelationFailed = "correlation failed";

  internal static string? ValidateCorrelationCookie(HttpRequest request, string correlationId)
  {
    var cookieName = GetCorrelationCookieName(correlationId);
    var cookieContent = GetCookieContent(request, cookieName);

    if(IsEmptyCorrelationContent(cookieContent)) return CorrelationCookieNotFound;
    if(!IsCorrelationContentMatch(cookieContent!)) return UnexpectedCorrelationCookieContent;
    return default;
  }

  public static string? ValidateCorrelationCookie(HttpRequest request, AuthenticationProperties authProps) {
    if(GetAuthPropsCorrelationId(authProps!) is not string correlationId) return CorrelationIdKeyNotFound;
    if(ValidateCorrelationCookie(request, correlationId) is string correlationError) return $"{CorrelationFailed} {correlationError}";
    return default;
  }
}