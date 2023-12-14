
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  internal const string AccessDenied = "Access was denied by the resource owner or by the remote server";
  internal const string AuthorizationCodeNotFound = "Authorization code was not found";
  internal const string CorrelationIdKeyNotFound = "Correlation id not found on authentication properties";
  internal const string CorrelationFailed = "Correlation failed";
  internal const string InvalidAuthorizationState = "OAuth state was missing or invalid";
  internal const string UnprotectAuthorizationStateFailed = "Unprotect OAuth state failed";

  static string? ValidateAuthorizationResultCodeAndState (HttpRequest request) {
    if (!ExistsAuthorizationCode(request)) return AuthorizationCodeNotFound;
    if (!ExistsAuthenticationState(request)) return InvalidAuthorizationState;
    return default;
  }

  static string? ValidateAuthorizationResultSuccess (HttpRequest request) {
    if (IsAccessDeniedAuthorizationError(request)) return AccessDenied;
    if (IsGenericAuthorizationError(request)) return BuildAuthorizationError(request);
    return default;
  }

  internal static string? ValidateAuthorizationCorrelationCookie (HttpContext context, AuthenticationProperties authProperties) {
    if (GetAuthenticationPropertiesCorrelationId(authProperties!) is not string correlationId) return CorrelationIdKeyNotFound;
    if (ValidateCorrelationCookie(context.Request, correlationId) is string correlationError) return $"{CorrelationFailed} {correlationError}";
    return default;
  }

  internal static string? ValidateAuthorizationResult (HttpContext context) {
    if (ValidateAuthorizationResultSuccess(context.Request) is string authorizationError) return authorizationError;
    if (ValidateAuthorizationResultCodeAndState(context.Request) is string missingError) return missingError;
    return default;
  }

}