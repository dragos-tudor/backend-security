using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsSafePostAuthorizationMessage(OpenIdConnectMessage oidcMessage) =>
    IsEmptyString(oidcMessage.AccessToken) && IsEmptyString(oidcMessage.IdToken);

  static bool IsSuccessPostAuthorizationMessage(OpenIdConnectMessage oidcMessage) =>
    oidcMessage.Error is null;
}