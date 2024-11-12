
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string ProtectAuthenticationProperties(AuthenticationProperties authProperties, PropertiesDataFormat propertiesDataFormat) => propertiesDataFormat.Protect(authProperties);
}