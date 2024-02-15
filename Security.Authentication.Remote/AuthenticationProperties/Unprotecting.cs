using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static AuthenticationProperties? UnprotectAuthenticationProperties(
    string encryptedProperties,
    PropertiesDataFormat propertiesDataFormat) =>
      propertiesDataFormat.Unprotect(encryptedProperties);
}