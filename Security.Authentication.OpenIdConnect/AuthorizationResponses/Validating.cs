using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? ValidateAuthorizationResponse(OpenIdConnectMessage oidcMessage)
  {
    if (IsSucceddedAuthorizationResponse(oidcMessage)) return default;
    if (IsAccessDeniedAuthorizationError(oidcMessage)) return AccessDeniedAuthorizationError;
    return BuildAuthorizationError(oidcMessage);
  }
}