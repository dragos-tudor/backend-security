
using System.Security.Claims;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static ClaimsPrincipal CreateClaimsPrincipal () =>
    new (new ClaimsIdentity());

}