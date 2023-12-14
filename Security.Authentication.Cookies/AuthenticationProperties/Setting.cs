
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static DateTimeOffset? SetAuthenticationPropertiesExpires (AuthenticationProperties authProperties, DateTimeOffset issuedUtc, TimeSpan? expires) =>
    expires is not null?
      authProperties.ExpiresUtc = issuedUtc.Add(expires.Value):
      authProperties.ExpiresUtc;

  static DateTimeOffset? SetAuthenticationPropertiesIssued (AuthenticationProperties authProperties, DateTimeOffset? currentUtc) =>
    authProperties.IssuedUtc = currentUtc;

}