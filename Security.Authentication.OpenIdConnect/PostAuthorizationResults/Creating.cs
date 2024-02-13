using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal static PostAuthorizationResult CreatePostAuthorizationFailure(string failure) =>
    new (default, failure);

  internal static PostAuthorizationResult CreatePostAuthorizationSuccess(
    AuthenticationProperties authProperties,
    string? authorizationCode = default,
    string? idToken = default,
    ClaimsIdentity? identity = default) =>
      new (new PostAuthorizationInfo(authProperties, authorizationCode, idToken, identity), default);
}