
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authorization;

partial class Funcs {

  static DateTimeOffset? MinimumAuthenticationPropertiesExpires (IEnumerable<AuthenticateResult> results) =>
    results.Aggregate(
      default(DateTimeOffset?),
      MininimumAuthenticationPropertiesExpire);

  static DateTimeOffset? MininimumAuthenticationPropertiesExpire (DateTimeOffset? expiresUtc, AuthenticateResult result) =>
    (expiresUtc is null || result.Properties?.ExpiresUtc < expiresUtc) ?
      result.Properties?.ExpiresUtc :
      expiresUtc;

}