using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  readonly static OpenIdConnectProtocolValidator ProtocolValidator = new ();

  static string? ValidatePostAuthorizationMessage(OpenIdConnectMessage oidcMessage)
  {
    if (IsSucceddedPostAuthorizationMessage(oidcMessage)) return default;
    if (IsAccessDeniedError(oidcMessage)) return AccessDeniedToken;
    return BuildGenericError(oidcMessage);
  }

  static void ValidatePostAuthorizationMessageProtocol(
    OpenIdConnectMessage oidcMessage,
    OpenIdConnectOptions oidcOptions,
    JwtSecurityToken? securityToken = default,
    string? nonce = default) =>
    ProtocolValidator.ValidateAuthenticationResponse(new OpenIdConnectProtocolValidationContext() {
      ProtocolMessage = oidcMessage,
      ClientId = oidcOptions.ClientId,
      ValidatedIdToken = securityToken,
      Nonce = nonce
    });
}