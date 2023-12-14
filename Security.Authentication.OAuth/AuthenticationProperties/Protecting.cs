
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  internal static string ProtectAuthenticationProperties (
    AuthenticationProperties authProperties,
    ISecureDataFormat<AuthenticationProperties> secureDataFormat) =>
      secureDataFormat.Protect(authProperties);

}