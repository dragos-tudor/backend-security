using System.IdentityModel.Tokens.Jwt;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string GetSecurityTokenNonce(JwtSecurityToken securityToken) =>
    securityToken.Payload.Nonce;
}