
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static bool ExistsPostAuthorizationCode (HttpRequest request) =>
    GetPostAuthorizationCode(request) is not null;

  static bool ExistsPostAuthorizationState (HttpRequest request) =>
    GetPostAuthorizationState(request) is not null;
}