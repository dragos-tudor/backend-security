
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class Funcs {

  internal static string ProtectAuthenticationProperties (AuthenticationProperties authProperties, ISecureDataFormat<AuthenticationProperties> propertiesDataFormat) =>
    propertiesDataFormat.Protect(authProperties);

}