using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static void ValidateUserInfoMessageProtocol(
    string userInfoResponse,
    JwtSecurityToken securityToken) =>
      CreateOpenIdConnectProtocolValidator()
        .ValidateUserInfoResponse(new OpenIdConnectProtocolValidationContext() {
          UserInfoEndpointResponse = userInfoResponse,
          ValidatedIdToken = securityToken
        });
}