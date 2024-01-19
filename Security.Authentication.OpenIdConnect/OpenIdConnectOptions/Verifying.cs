using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsCodeOpenIdConnectResponseType(OpenIdConnectOptions oidcOptions) =>
    string.Equals(oidcOptions.ResponseType, OpenIdConnectResponseType.Code, StringComparison.Ordinal);

  static bool IsQueryOpenIdConnectResponseMode(OpenIdConnectOptions oidcOptions) =>
    string.Equals(oidcOptions.ResponseMode, OpenIdConnectResponseMode.Query, StringComparison.Ordinal);

  static bool IsRedirectGetOpenIdConnectAuthenticationMethod(OpenIdConnectOptions oidcOptions) =>
    oidcOptions.AuthenticationMethod == OpenIdConnectRedirectBehaviour.RedirectGet;

  static bool IsFormPostOpenIdConnectAuthenticationMethod(OpenIdConnectOptions oidcOptions) =>
    oidcOptions.AuthenticationMethod == OpenIdConnectRedirectBehaviour.FormPost;
}