
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? GetAuthenticationPropertiesItem (AuthenticationProperties authProperties, string key) =>
    authProperties.GetString(key);

  public static T? GetAuthenticationPropertiesParam<T> (AuthenticationProperties authProperties, string key) =>
    authProperties.GetParameter<T>(key);

  public static DateTimeOffset? GetAuthenticationPropertiesExpires (AuthenticationProperties authProperties) =>
    authProperties.ExpiresUtc?.ToUniversalTime();

  public static TimeSpan? GetAuthenticationPropertiesExpiresAfter (AuthenticationProperties authProperties) =>
    authProperties.ExpiresUtc - authProperties.IssuedUtc;

  public static string? GetAuthenticationPropertiesRedirectUri (AuthenticationProperties authProperties) =>
    authProperties.RedirectUri;
}