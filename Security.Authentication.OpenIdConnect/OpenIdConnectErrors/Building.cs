using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? BuildAccessDeniedErrorPath (AuthenticationProperties authProperties, OpenIdConnectOptions oidcOptions) =>
    AddQueryString(oidcOptions.AccessDeniedPath, oidcOptions.ReturnUrlParameter, authProperties.RedirectUri);

  static string BuildGenericError (OpenIdConnectMessage oidcMessage) =>
    $"Message contains error: '{oidcMessage.Error}', error_description: '{oidcMessage.ErrorDescription}', error_uri: '{oidcMessage.ErrorUri}'.";
}