namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static TokenInfo? GetTokenInfo(TokenResult? tokenResult) =>
    tokenResult?.Success;
}