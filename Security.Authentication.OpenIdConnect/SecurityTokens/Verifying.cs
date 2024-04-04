using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsJsonWebToken(SecurityToken securityToken) =>
    securityToken is JsonWebToken;

  static bool IsSetSecurityTokenValidFrom(SecurityToken securityToken) =>
    securityToken.ValidFrom > DateTime.MinValue;

  static bool IsSetSecurityTokenValidTo(SecurityToken securityToken) =>
    securityToken.ValidTo > DateTime.MinValue;
}