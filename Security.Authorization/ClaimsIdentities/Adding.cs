
using System.Collections.Generic;
using System.Security.Claims;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static ClaimsPrincipal AddAuthenticatedClaimsIdentities (this ClaimsPrincipal first, ClaimsPrincipal? second) =>
    second?.Identities is not null?
      AddClaimsIdentities(first, GetAuthenticatedClaimsIdentities(second)):
      first;

  static ClaimsPrincipal AddClaimsIdentities (ClaimsPrincipal principal, IEnumerable<ClaimsIdentity> identities) {
    principal.AddIdentities(identities);
    return principal;
  }

  static ClaimsPrincipal AddClaimsIdentities (this ClaimsPrincipal first, ClaimsPrincipal? second) =>
    second?.Identities is not null?
      AddClaimsIdentities(first, second.Identities):
      first;

}