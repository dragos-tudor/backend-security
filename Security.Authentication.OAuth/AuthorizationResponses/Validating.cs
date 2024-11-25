
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal const string AuthorizationCodeNotFound = "authorization code was not found";
  internal const string InvalidState = "oauth state was missing or invalid";
  internal const string UnprotectStateFailed = "unprotect oauth state failed";

  internal static string? ValidateAuthorizationResponse(HttpRequest request)
  {
    if (IsOAuthError(request)) return GetOAuthErrorType(request);
    if (!ExistsAuthorizationCode(request)) return AuthorizationCodeNotFound;
    if (!ExistsAuthorizationState(request)) return InvalidState;
    return default;
  }
}