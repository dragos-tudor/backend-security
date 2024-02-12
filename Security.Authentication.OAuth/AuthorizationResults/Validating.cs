
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  internal const string AccessDenied = "Access was denied by the resource owner or by the remote server";
  internal const string AuthorizationCodeNotFound = "Authorization code was not found";
  internal const string InvalidAuthorizationState = "OAuth state was missing or invalid";
  internal const string UnprotectAuthorizationStateFailed = "Unprotect OAuth state failed";

  static string? ValidateAuthorizationResultCodeAndState (HttpRequest request) {
    if (!ExistsAuthorizationCode(request)) return AuthorizationCodeNotFound;
    if (!ExistsAuthenticationState(request)) return InvalidAuthorizationState;
    return default;
  }

  static string? ValidateAuthorizationResultSuccedded (HttpRequest request) {
    if (IsAccessDeniedAuthorizationError(request)) return AccessDenied;
    if (IsGenericAuthorizationError(request)) return BuildAuthorizationError(request);
    return default;
  }

  internal static string? ValidateAuthorizationResult (HttpContext context) {
    if (ValidateAuthorizationResultSuccedded(context.Request) is string authorizationError) return authorizationError;
    if (ValidateAuthorizationResultCodeAndState(context.Request) is string missingError) return missingError;
    return default;
  }

}