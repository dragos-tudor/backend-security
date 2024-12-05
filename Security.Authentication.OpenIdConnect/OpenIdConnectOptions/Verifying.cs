using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsFormPostAuthMethod(OpenIdConnectOptions oidcOptions) => oidcOptions.AuthenticationMethod == OpenIdConnectRedirectBehaviour.FormPost;

  static bool IsRedirectGetAuthMethod(OpenIdConnectOptions oidcOptions) => oidcOptions.AuthenticationMethod == OpenIdConnectRedirectBehaviour.RedirectGet;

  static bool IsQueryResponseMode(OpenIdConnectOptions oidcOptions) => string.Equals(oidcOptions.ResponseMode, OpenIdConnectResponseMode.Query, StringComparison.Ordinal);


  static bool ShouldGetUserInfoClaims(OpenIdConnectOptions oidcOptions) => oidcOptions.GetClaimsFromUserInfoEndpoint;

  static bool ShouldUseTokenLifetime(OpenIdConnectOptions oidcOptions) => oidcOptions.UseTokenLifetime;
}