using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static AuthenticationProperties? UnprotectAuthenticationProperties(string encryptedProperties, PropertiesDataFormat propertiesDataFormat) => propertiesDataFormat.Unprotect(encryptedProperties);
}