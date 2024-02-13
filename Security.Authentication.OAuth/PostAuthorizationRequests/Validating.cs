
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal const string PostAuthorizationCodeNotFound = "Authorization code was not found";
  internal const string InvalidPostAuthorizationState = "OAuth state was missing or invalid";
  internal const string UnprotectAuthorizationStateFailed = "Unprotect OAuth state failed";

  static string? ValidatePostAuthorizationCodeAndState (HttpRequest request) {
    if (!ExistsPostAuthorizationCode(request)) return PostAuthorizationCodeNotFound;
    if (!ExistsPostAuthorizationState(request)) return InvalidPostAuthorizationState;
    return default;
  }

  internal static string? ValidatePostAuthorizationRequest (HttpContext context) {
    if (GetPostAuthorizationError(context.Request) is string authError) return authError;
    if (ValidatePostAuthorizationCodeAndState(context.Request) is string missingError) return missingError;
    return default;
  }

}