
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static DateTimeOffset? SetAuthenticationPropertiesExpires (
    AuthenticationProperties authProperties,
    DateTimeOffset currentUtc,
    TimeSpan expiresAfter
  ) =>
    authProperties.ExpiresUtc = currentUtc + expiresAfter;
}