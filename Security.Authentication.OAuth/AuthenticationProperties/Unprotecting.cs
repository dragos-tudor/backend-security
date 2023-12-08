
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class Funcs {

  internal static AuthenticationProperties? UnprotectAuthenticationProperties (string? encryptedProperties, ISecureDataFormat<AuthenticationProperties> propertiesDataFormat) =>
    propertiesDataFormat.Unprotect(encryptedProperties);

}