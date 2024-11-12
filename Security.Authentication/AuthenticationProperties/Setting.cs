
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string SetAuthenticationPropertiesItem(AuthenticationProperties authProperties, string key, string value) { authProperties.SetString(key, value); return value; }

  public static T SetAuthenticationPropertiesParam<T>(AuthenticationProperties authProperties, string key, T value) { authProperties.SetParameter(key, value); return value; }

  public static DateTimeOffset? SetAuthenticationPropertiesExpires(AuthenticationProperties authProperties, DateTimeOffset expires) => authProperties.ExpiresUtc = expires;

  public static DateTimeOffset? SetAuthenticationPropertiesExpires(AuthenticationProperties authProperties, DateTimeOffset issuedUtc, TimeSpan? expiresAfter) =>
    expiresAfter is not null? authProperties.ExpiresUtc = issuedUtc.Add(expiresAfter.Value): authProperties.ExpiresUtc;

  public static DateTimeOffset? SetAuthenticationPropertiesIssued(AuthenticationProperties authProperties, DateTimeOffset issued) => authProperties.IssuedUtc = issued;

  public static string SetAuthenticationPropertiesRedirectUri(AuthenticationProperties authProperties, string redirectUri) => authProperties.RedirectUri ??= redirectUri;
}