using System.IdentityModel.Tokens.Jwt;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static ClaimsIdentity GetClaimsIdentity(PostAuthorizationInfo? authInfo, TokenInfo? tokenInfo) =>
    (tokenInfo?.Identity ?? authInfo?.Identity)!;

  static string GetIdToken(PostAuthorizationInfo? authInfo, TokenInfo? tokenInfo) =>
    (tokenInfo?.IdToken ?? authInfo?.IdToken)!;

  static JwtSecurityToken GetSecurityToken(PostAuthorizationInfo? authInfo, TokenInfo? tokenInfo) =>
    (tokenInfo?.SecurityToken ?? authInfo?.SecurityToken)!;
}