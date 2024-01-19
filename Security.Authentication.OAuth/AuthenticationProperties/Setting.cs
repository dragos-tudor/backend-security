
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static void SetAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties, string codeVerifier) =>
    authProperties.Items.Add(CodeVerifier, codeVerifier);

}