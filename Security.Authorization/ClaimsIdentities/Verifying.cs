
using System.Linq;
using System.Security.Claims;

namespace Security.Authorization;

partial class Funcs {

  static bool IsAuthenticatedClaimsIdentity (ClaimsIdentity identity) =>
    identity.IsAuthenticated ||
    identity.Claims.Any();

}