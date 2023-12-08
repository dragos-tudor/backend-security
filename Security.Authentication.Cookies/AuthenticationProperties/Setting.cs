
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class Funcs {

  static DateTimeOffset? SetAuthenticationPropertiesExpires (AuthenticationProperties authProperties, TimeSpan expires, DateTimeOffset? issuedUtc) =>
    authProperties.ExpiresUtc = issuedUtc?.Add(expires);

  static DateTimeOffset? SetAuthenticationPropertiesIssued (AuthenticationProperties authProperties, DateTimeOffset? currentUtc) =>
    authProperties.IssuedUtc = currentUtc;

}