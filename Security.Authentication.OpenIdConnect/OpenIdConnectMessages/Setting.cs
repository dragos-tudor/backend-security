using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static void SetOpenIdConnectMessageResponseMode(OpenIdConnectMessage oidcMessage, string responseMode) =>
    oidcMessage.ResponseMode = responseMode;

  static void SetOpenIdConnectMessageState(OpenIdConnectMessage oidcMessage, string state) =>
    oidcMessage.State = state;
}