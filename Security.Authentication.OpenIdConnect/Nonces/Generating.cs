using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string GenerateNonce(OpenIdConnectProtocolValidator protocolValidator) =>
    protocolValidator.GenerateNonce();
}