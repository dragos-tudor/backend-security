using System.Linq;
using System.Security.Claims;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static string? GetUserSchemeName (ClaimsPrincipal principal) =>
    GetPrincipalIdentity(principal)?.AuthenticationType;

  static string[] GetUserClaims (ClaimsPrincipal principal) =>
    principal.Claims.Select(claim => claim.Value).ToArray();
}