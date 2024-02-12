using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsSafePostAuthorizeResponse(OpenIdConnectMessage oidcMessage) =>
    IsEmptyString(oidcMessage.AccessToken) && IsEmptyString(oidcMessage.IdToken);

  static bool IsSucceddedPostAuthorizeResponse(OpenIdConnectMessage oidcMessage) =>
    oidcMessage.Error is null;
}