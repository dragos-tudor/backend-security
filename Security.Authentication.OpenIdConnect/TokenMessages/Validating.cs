using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? ValidateTokenMessage(OpenIdConnectMessage oidcMessage)
  {
    if (!IsSuccessTokenMessage(oidcMessage)) return BuildGenericError(oidcMessage);
    return default;
  }

  static void ValidateTokenMessageProtocol(
    OpenIdConnectMessage oidcMessage,
    OpenIdConnectOptions oidcOptions,
    JwtSecurityToken? securityToken = default,
    string? nonce = default) =>
      CreateOpenIdConnectProtocolValidator()
        .ValidateTokenResponse(new OpenIdConnectProtocolValidationContext() {
          ProtocolMessage = oidcMessage,
          ClientId = oidcOptions.ClientId,
          ValidatedIdToken = securityToken,
          Nonce = nonce
        });
}