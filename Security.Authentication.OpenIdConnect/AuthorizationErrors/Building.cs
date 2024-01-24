using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal const string AuthorizationEndpointError = "Message contains error: '{0}', error_description: '{1}', error_uri: '{2}'.";

  static string? BuildAccessDeniedErrorPath(AuthenticationProperties authProperties, OpenIdConnectOptions oidcOptions) =>
    AddQueryString(oidcOptions.AccessDeniedPath, oidcOptions.ReturnUrlParameter, authProperties.RedirectUri);

  static string BuildAuthorizationError (OpenIdConnectMessage oidcMessage) =>
    string.Format(AuthorizationEndpointError, oidcMessage.Error, oidcMessage.ErrorDescription, oidcMessage.ErrorUri);
}