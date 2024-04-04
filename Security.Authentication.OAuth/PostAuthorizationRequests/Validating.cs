
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal const string PostAuthorizationCodeNotFound = "Authorization code was not found";
  internal const string InvalidPostAuthorizationState = "OAuth state was missing or invalid";
  internal const string UnprotectAuthorizationStateFailed = "Unprotect OAuth state failed";

  internal static string? ValidatePostAuthorizationRequest (HttpContext context) {
    if (GetPostAuthorizationError(context.Request) is string authError) return authError;
    if (!ExistsPostAuthorizationCode(context.Request)) return PostAuthorizationCodeNotFound;
    if (!ExistsPostAuthorizationState(context.Request)) return InvalidPostAuthorizationState;
    return default;
  }

}