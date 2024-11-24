using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsJsonWebToken(SecurityToken securityToken) => securityToken is JsonWebToken;

  static bool HasSecurityTokenValidFrom(SecurityToken securityToken) => securityToken.ValidFrom > DateTime.MinValue;

  static bool HasSecurityTokenValidTo(SecurityToken securityToken) => securityToken.ValidTo > DateTime.MinValue;
}