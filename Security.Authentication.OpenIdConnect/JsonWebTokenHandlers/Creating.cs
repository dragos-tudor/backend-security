using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static JsonWebTokenHandler CreateJsonWebTokenHandler() =>
    new () { MapInboundClaims = JwtSecurityTokenHandler.DefaultMapInboundClaims };
}