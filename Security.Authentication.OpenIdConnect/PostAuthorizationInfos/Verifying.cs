namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool ExistsPostAuthorizationIdentity(PostAuthorizationInfo authInfo) =>
    authInfo.Identity is null;

  static bool ExistsPostAuthorizationCode(PostAuthorizationInfo authInfo) =>
    IsNotEmptyString(authInfo.Code);
}