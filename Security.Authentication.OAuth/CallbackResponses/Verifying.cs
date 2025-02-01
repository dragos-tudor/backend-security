
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool ExistsCallbackAuthorizationCode(HttpRequest request) => GetCallbackAuthorizationCode(request) is not null;

  static bool ExistsCallbackState(HttpRequest request) => GetCallbackState(request) is not null;
}