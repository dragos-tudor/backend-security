using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial record class PostAuthorizationResult
{
  public static implicit operator PostAuthorizationResult(AuthenticationProperties authProperties) =>
    CreatePostAuthorizationResultSuccess(authProperties);

  public static implicit operator PostAuthorizationResult(string failure) =>
    CreatePostAuthorizationResultFailure(failure);
}