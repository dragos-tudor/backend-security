
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  internal const string AuthorizationCodeKey = "code";

  static string? GetAuthorizationCode (HttpRequest request) =>
    request.Query[AuthorizationCodeKey];

  static string? GetAuthorizationState (HttpRequest request) =>
    request.Query[State];

}