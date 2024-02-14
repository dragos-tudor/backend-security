using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsOpenIdConnectCodeFlow(OpenIdConnectMessage oidcMessage) =>
    IsNotEmptyString(oidcMessage.Code);

  static bool IsOpenIdConnectHybridFlow(OpenIdConnectMessage oidcMessage) =>
    IsNotEmptyString(oidcMessage.IdToken) && IsNotEmptyString(oidcMessage.Code);

  static bool IsOpenIdConnectImplicitFlow(OpenIdConnectMessage oidcMessage) =>
    IsNotEmptyString(oidcMessage.IdToken);

  static bool IsOpenIdConnectImplicitOrHybridFlow(OpenIdConnectMessage oidcMessage) =>
    IsOpenIdConnectImplicitFlow(oidcMessage) || IsOpenIdConnectHybridFlow(oidcMessage);
}