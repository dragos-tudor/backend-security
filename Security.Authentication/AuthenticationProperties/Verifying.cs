
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static bool ExistsAuthenticationProperties(AuthenticationProperties? authProperties) => authProperties is not null;

  public static bool ExistsAuthenticationPropertyItem(AuthenticationProperties authProperties, string key) => authProperties.Items.ContainsKey(key);

  public static bool IsAuthenticationPropertiesPersistent(AuthenticationProperties authProperties) => authProperties?.IsPersistent ?? false;
}