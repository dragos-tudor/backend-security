using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal const string AccessDeniedAuthorizationError = "access_denied";

  static bool IsAccessDeniedAuthorizationError (OpenIdConnectMessage oidcMessage) =>
    string.Equals(oidcMessage.Error, AccessDeniedAuthorizationError, StringComparison.Ordinal);
}