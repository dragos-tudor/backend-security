using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static JwtSecurityToken ToJwtSecurityToken(SecurityToken securityToken) =>
    JwtSecurityTokenConverter.Convert(securityToken as JsonWebToken);
}