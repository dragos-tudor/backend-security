
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool ExistsAuthorizationCode(HttpRequest request) => GetAuthorizationCode(request) is not null;

  static bool ExistsAuthorizationState(HttpRequest request) => GetAuthorizationState(request) is not null;
}