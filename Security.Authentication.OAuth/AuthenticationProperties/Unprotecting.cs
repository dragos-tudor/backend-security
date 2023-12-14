
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  internal static AuthenticationProperties? UnprotectAuthenticationProperties (
    string? encryptedProperties,
    ISecureDataFormat<AuthenticationProperties> secureDataFormat) =>
      secureDataFormat.Unprotect(encryptedProperties);

}