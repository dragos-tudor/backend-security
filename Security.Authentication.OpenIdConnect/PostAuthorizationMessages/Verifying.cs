using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsSafePostAuthorizationMessage(OpenIdConnectMessage oidcMessage) =>
    IsEmptyString(oidcMessage.AccessToken) && IsEmptyString(oidcMessage.IdToken);

  static bool IsSucceddedPostAuthorizationMessage(OpenIdConnectMessage oidcMessage) =>
    oidcMessage.Error is null;
}