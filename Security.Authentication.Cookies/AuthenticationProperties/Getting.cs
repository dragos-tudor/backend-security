
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static DateTimeOffset? GetAuthenticationPropertiesExpires (AuthenticationProperties authProperties) =>
    authProperties.ExpiresUtc?.ToUniversalTime();

  static string? GetAuthenticationPropertiesRedirectUri (AuthenticationProperties authProperties) =>
    authProperties.RedirectUri;

}