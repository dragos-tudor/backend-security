
using System.Collections.Generic;
using System.Security.Claims;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static ClaimsPrincipal AddAuthenticatedIdentities (this ClaimsPrincipal first, ClaimsPrincipal? second) =>
    second?.Identities is not null?
      AddIdentities(first, GetAuthenticatedIdentities(second)):
      first;

  static ClaimsPrincipal AddIdentities (ClaimsPrincipal principal, IEnumerable<ClaimsIdentity> identities) {
    principal.AddIdentities(identities);
    return principal;
  }

  static ClaimsPrincipal AddIdentities (this ClaimsPrincipal first, ClaimsPrincipal? second) =>
    second?.Identities is not null?
      AddIdentities(first, second.Identities):
      first;

}