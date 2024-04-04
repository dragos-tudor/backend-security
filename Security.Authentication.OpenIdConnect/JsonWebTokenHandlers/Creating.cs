using System.IdentityModel.Tokens.Jwt;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static JwtSecurityTokenHandler CreateJsonWebTokenHandler() =>
    new () { MapInboundClaims = JwtSecurityTokenHandler.DefaultMapInboundClaims };
}