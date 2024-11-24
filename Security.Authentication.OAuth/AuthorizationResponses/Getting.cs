
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static string? GetAuthorizationCode(HttpRequest request) => request.Query[OAuthParamNames.AuthorizationCode];

  static string? GetAuthorizationState(HttpRequest request) => request.Query[OAuthParamNames.State];
}