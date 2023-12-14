
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static void SetAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties, string codeVerifier) =>
    authProperties.Items.Add(CodeVerifier, codeVerifier);

}