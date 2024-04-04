
namespace Security.Authentication.OpenIdConnect;

partial record class PostAuthorizationResult
{
  public static implicit operator PostAuthorizationResult(PostAuthorizationInfo authInfo) =>
    CreatePostAuthorizationSuccess(authInfo);

  public static implicit operator PostAuthorizationResult(string failure) =>
    CreatePostAuthorizationFailure(failure);
}