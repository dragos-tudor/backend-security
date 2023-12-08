
using System.Security.Claims;

namespace Security.Authorization;

partial class Funcs {

  static ClaimsPrincipal CreateClaimsPrincipal () =>
    new (new ClaimsIdentity());

}