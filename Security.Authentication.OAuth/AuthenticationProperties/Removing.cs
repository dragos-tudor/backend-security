
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static void RemoveAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties) =>
    authProperties.Items.Remove(CodeVerifier);

}