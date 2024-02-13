using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal static PostAuthorizationResult CreatePostAuthorizationResultSuccess(AuthenticationProperties authProperties) =>
    new (authProperties);

  internal static PostAuthorizationResult CreatePostAuthorizationResultFailure(string failure) =>
    new (default, failure);
}