
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static string? GetAuthenticationPropertiesCallbackUri (AuthenticationProperties authProperties) =>
    authProperties.Items.TryGetValue(CallbackUri, out string? callbackUri)?
      callbackUri:
      default;

}