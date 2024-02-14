using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsSucceddedTokenMessage(OpenIdConnectMessage oidcMessage) =>
    oidcMessage.Error is null;
}