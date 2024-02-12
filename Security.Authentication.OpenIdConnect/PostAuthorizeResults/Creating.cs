using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal static PostAuthorizeResult CreatePostAuthorizeFailure(string failure) =>
    new (default, failure);

  internal static PostAuthorizeResult CreatePostAuthorizeSuccess(
    AuthenticationProperties authProperties,
    string? authorizationCode = default,
    string? idToken = default,
    ClaimsIdentity? identity = default) =>
      new (new PostAuthorizeInfo(authProperties, authorizationCode, idToken, identity), default);
}