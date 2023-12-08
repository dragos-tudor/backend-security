
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class Funcs {

  static void SetAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties, string codeVerifier) =>
    authProperties.Items.Add(CodeVerifier, codeVerifier);

}