using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static AuthenticationProperties? UnprotectAuthProps(string encryptedProps, PropertiesDataFormat propDataProtector) => propDataProtector.Unprotect(encryptedProps);
}