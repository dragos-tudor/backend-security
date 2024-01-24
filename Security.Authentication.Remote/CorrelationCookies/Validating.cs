
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  internal const string CorrelationCookieNotFound = "Correlation cookie not found";
  internal const string UnexpectedCorrelationCookieContent = "Unexpected correlation cookie";

  public static string? ValidateCorrelationCookie (HttpRequest request, string correlationId)
  {
    var cookieName = GetCorrelationCookieName(correlationId);
    var cookieContent = GetCorrelationCookieContent(request, cookieName);

    if (IsEmptyCorrelationContent(cookieContent)) return CorrelationCookieNotFound;
    if (!IsCorrelationContentMatch(cookieContent!)) return UnexpectedCorrelationCookieContent;
    return default;
  }

  public static string? ValidateCorrelationCookie (HttpRequest request, AuthenticationProperties authProperties) =>
    ValidateCorrelationCookie(request, GetAuthenticationPropertiesCorrelationId(authProperties)!);
}