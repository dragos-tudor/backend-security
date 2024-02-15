
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static string ProtectAuthenticationProperties(
    AuthenticationProperties authProperties,
    PropertiesDataFormat propertiesDataFormat) =>
      propertiesDataFormat.Protect(authProperties);
}