using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal const string AccessDeniedToken = "access_denied";

  static bool IsAccessDeniedError (OpenIdConnectMessage oidcMessage) =>
    string.Equals(oidcMessage.Error, AccessDeniedToken, StringComparison.OrdinalIgnoreCase);
}