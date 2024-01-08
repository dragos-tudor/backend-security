
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static DateTimeOffset? SetAuthenticationPropertiesExpires (
    AuthenticationProperties authProperties,
    DateTimeOffset currentUtc,
    TimeSpan expires
  ) =>
    authProperties.ExpiresUtc = currentUtc.Add(expires);
}