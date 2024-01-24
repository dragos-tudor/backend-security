using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsValidSecurityToken(TokenValidationResult validationResult, SecurityToken securityToken) =>
    validationResult.IsValid && securityToken is not null;

  static bool IsJwtSecurityToken(SecurityToken securityToken) =>
    securityToken is JsonWebToken;
}