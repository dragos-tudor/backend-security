
using Microsoft.AspNetCore.Authentication;
#pragma warning disable CA1854

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  internal static string? GetAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties) =>
    authProperties.Items.ContainsKey(CodeVerifier)?
      authProperties.Items[CodeVerifier]!:
      default;

  static string? GetAuthenticationPropertiesCallbackUri (AuthenticationProperties authProperties) =>
    authProperties.Items.ContainsKey(CallbackUri)?
      authProperties.Items[CallbackUri]!:
      default;

}