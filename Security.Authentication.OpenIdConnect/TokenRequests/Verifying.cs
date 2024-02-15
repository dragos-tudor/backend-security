namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool ShouldExchangeCodeForTokens(PostAuthorizationInfo authInfo) =>
    ExistsPostAuthorizationCode(authInfo) && !ExistsPostAuthorizationIdentity(authInfo);
}