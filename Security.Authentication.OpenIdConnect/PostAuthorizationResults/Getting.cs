namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static PostAuthorizationInfo? GetPostAuthorizationInfo(PostAuthorizationResult authResult) =>
    authResult.Success;
}