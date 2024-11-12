
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static DateTimeOffset? SetAuthenticationPropertiesExpiration(AuthenticationProperties authProperties, DateTimeOffset currentUtc, TimeSpan? expiresAfter)
  {
    SetAuthenticationPropertiesIssued(authProperties, currentUtc);
    return SetAuthenticationPropertiesExpires(authProperties, currentUtc, expiresAfter);
  }
}