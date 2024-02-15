
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static bool RemoveAuthenticationPropertiesItem (AuthenticationProperties authProperties, string key) =>
    authProperties.Items.Remove(key);

  public static bool RemoveAuthenticationPropertiesCodeVerifier (AuthenticationProperties authProperties) =>
    RemoveAuthenticationPropertiesItem(authProperties, CodeVerifier);

  public static bool RemoveAuthenticationPropertiesCorrelationId (AuthenticationProperties authProperties) =>
    RemoveAuthenticationPropertiesItem(authProperties, CorrelationIdKey);
}