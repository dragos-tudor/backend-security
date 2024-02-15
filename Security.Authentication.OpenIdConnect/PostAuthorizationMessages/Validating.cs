using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? ValidatePostAuthorizationMessage(OpenIdConnectMessage oidcMessage)
  {
    if (IsAccessDeniedError(oidcMessage)) return AccessDeniedToken;
    if (!IsSuccessPostAuthorizationMessage(oidcMessage)) return BuildGenericError(oidcMessage);
    return default;
  }

  static void ValidatePostAuthorizationMessageProtocol(
    OpenIdConnectMessage oidcMessage,
    OpenIdConnectOptions oidcOptions,
    JwtSecurityToken? securityToken = default,
    string? nonce = default) =>
      CreateOpenIdConnectProtocolValidator()
        .ValidateAuthenticationResponse(new OpenIdConnectProtocolValidationContext() {
          ProtocolMessage = oidcMessage,
          ClientId = oidcOptions.ClientId,
          ValidatedIdToken = securityToken,
          Nonce = nonce
        });
}