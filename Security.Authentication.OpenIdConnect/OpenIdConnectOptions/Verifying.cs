using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsCodeOpenIdConnectResponseType(OpenIdConnectOptions oidcOptions) =>
    string.Equals(oidcOptions.ResponseType, OpenIdConnectResponseType.Code, StringComparison.Ordinal);

  static bool IsFormPostOpenIdConnectAuthenticationMethod(OpenIdConnectOptions oidcOptions) =>
    oidcOptions.AuthenticationMethod == OpenIdConnectRedirectBehaviour.FormPost;

  static bool IsQueryOpenIdConnectResponseMode(OpenIdConnectOptions oidcOptions) =>
    string.Equals(oidcOptions.ResponseMode, OpenIdConnectResponseMode.Query, StringComparison.Ordinal);

  static bool IsRedirectGetOpenIdConnectAuthenticationMethod(OpenIdConnectOptions oidcOptions) =>
    oidcOptions.AuthenticationMethod == OpenIdConnectRedirectBehaviour.RedirectGet;

  static bool IsResponseModeOpenIdConnectSettable(OpenIdConnectOptions oidcOptions) =>
    !IsCodeOpenIdConnectResponseType(oidcOptions) || !IsQueryOpenIdConnectResponseMode(oidcOptions);

  static bool ShouldSaveTokens(OpenIdConnectOptions oidcOptions) =>
    oidcOptions.SaveTokens;

  static bool ShouldCleanNonce(OpenIdConnectOptions oidcOptions) =>
    oidcOptions.RequireNonce;

  static bool ShouldUseNonce(OpenIdConnectOptions oidcOptions) =>
    oidcOptions.RequireNonce;

  static bool ShouldUseTokenLifetime(OpenIdConnectOptions oidcOptions) =>
    oidcOptions.UseTokenLifetime;
}