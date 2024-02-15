using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsSuccessTokenMessage(OpenIdConnectMessage oidcMessage) =>
    oidcMessage.Error is null;
}