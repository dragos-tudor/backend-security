namespace Security.Authentication.OpenIdConnect;

partial record class PostAuthorizeResult
{
  public static implicit operator PostAuthorizeResult(string failure) =>
    CreatePostAuthorizeFailure(failure);
}