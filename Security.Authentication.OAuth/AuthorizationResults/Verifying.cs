
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static bool ExistsAuthorizationCode (HttpRequest request) =>
    GetAuthorizationCode(request) is not null;

  static bool ExistsAuthenticationState (HttpRequest request) =>
    GetAuthorizationState(request) is not null;

  public static bool IsAccessDeniedError (string? error) =>
    error == AccessDenied;

}