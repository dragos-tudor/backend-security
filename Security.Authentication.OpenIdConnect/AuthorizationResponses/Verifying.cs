using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsSafeAuthorizationResponse(OpenIdConnectMessage oidcMessage) =>
    IsEmptyString(oidcMessage.AccessToken) && IsEmptyString(oidcMessage.IdToken);

  static bool IsSucceddedAuthorizationResponse(OpenIdConnectMessage oidcMessage) =>
    oidcMessage.Error is null;
}