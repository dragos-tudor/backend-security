
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static bool RemoveAuthenticationPropertiesItem (AuthenticationProperties authProperties, string key) =>
    authProperties.Items.Remove(key);
}