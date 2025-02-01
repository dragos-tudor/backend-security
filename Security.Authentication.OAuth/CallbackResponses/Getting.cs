
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  readonly static string NoCallbackRedirect = String.Empty;

  static string? GetCallbackAuthorizationCode(HttpRequest request) => request.Query[OAuthParamNames.AuthorizationCode];

  static string? GetCallbackState(HttpRequest request) => request.Query[OAuthParamNames.State];
}