
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class Funcs {

  static void RemoveAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties) =>
    authProperties.Items.Remove(CodeVerifier);

}