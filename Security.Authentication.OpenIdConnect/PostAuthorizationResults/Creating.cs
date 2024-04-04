
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal static PostAuthorizationResult CreatePostAuthorizationFailure(string failure) =>
    new (default, failure);

  internal static PostAuthorizationResult CreatePostAuthorizationSuccess(PostAuthorizationInfo authInfo) =>
      new (authInfo, default);
}