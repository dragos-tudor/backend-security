
using System.Collections.Generic;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static ClaimsPrincipal CreatePrincipal(string schemeName, IEnumerable<Claim>? claims = default) => new(CreateIdentity(schemeName, claims));

  public static ClaimsPrincipal CreatePrincipal(ClaimsIdentity identity) => new(identity);

  public static ClaimsPrincipal CreateDefaultPrincipal() => new(new ClaimsIdentity());
}