
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static TokenInfo CreateTokenInfo(
    OpenIdConnectMessage oidcMessage,
    TokenValidationResult validationResult,
    JwtSecurityToken securityToken) =>
      new (oidcMessage.IdToken, oidcMessage.AccessToken, oidcMessage.TokenType, oidcMessage.RefreshToken,
        validationResult.ClaimsIdentity, securityToken);
}