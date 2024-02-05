
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static string? GetAuthenticationPropertiesCallbackUri (AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesItem(authProperties, CallbackUri);

}