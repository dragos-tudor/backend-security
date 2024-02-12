using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? ValidatePostAuthorizeResponse(OpenIdConnectMessage oidcMessage)
  {
    if (IsSucceddedPostAuthorizeResponse(oidcMessage)) return default;
    if (IsAccessDeniedAuthorizationError(oidcMessage)) return AccessDeniedAuthorizationError;
    return BuildAuthorizationError(oidcMessage);
  }

  static void ValidatePostAuthorizeResponseProtocol(
    OpenIdConnectMessage oidcMessage,
    OpenIdConnectOptions oidcOptions,
    OpenIdConnectProtocolValidator protocolValidator,
    JwtSecurityToken? securityToken = default,
    string? nonce = default) =>
    protocolValidator.ValidateAuthenticationResponse(new OpenIdConnectProtocolValidationContext() {
      ProtocolMessage = oidcMessage,
      ClientId = oidcOptions.ClientId,
      ValidatedIdToken = securityToken,
      Nonce = nonce
    });
}