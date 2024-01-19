
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static string? GetAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties) =>
    authProperties.Items.TryGetValue(CodeVerifier, out string? codeVerifier)?
      codeVerifier:
      default;

  static string? GetAuthenticationPropertiesCallbackUri (AuthenticationProperties authProperties) =>
    authProperties.Items.TryGetValue(CallbackUri, out string? callbackUri)?
      callbackUri:
      default;

}