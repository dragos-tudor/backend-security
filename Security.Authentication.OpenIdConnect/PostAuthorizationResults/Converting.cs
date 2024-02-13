using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

partial record class PostAuthorizationResult
{
  public static implicit operator PostAuthorizationResult(AuthenticationProperties authProperties) =>
    CreatePostAuthorizationSuccess(authProperties);

  public static implicit operator PostAuthorizationResult(string failure) =>
    CreatePostAuthorizationFailure(failure);
}