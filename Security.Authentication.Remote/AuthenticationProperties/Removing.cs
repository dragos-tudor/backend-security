
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  public static void RemoveAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties) =>
    authProperties.Items.Remove(CodeVerifier);

}