
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal const string AuthorizationCodeKey = "code";

  static string? GetPostAuthorizationCode (HttpRequest request) =>
    request.Query[AuthorizationCodeKey];

  static string? GetPostAuthorizationState (HttpRequest request) =>
    request.Query[State];

  static string? GetPostAuthorizationError (HttpRequest request) {
    if (IsAccessDeniedError(request)) return AccessDeniedError;
    if (IsGenericError(request)) return BuildGenericError(request);
    return default;
  }
}