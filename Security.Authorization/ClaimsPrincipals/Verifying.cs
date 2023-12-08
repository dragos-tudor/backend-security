
using System.Security.Claims;

namespace Security.Authorization;

partial class Funcs {

  static bool IsAuthenticatedClaimsPrincipal (ClaimsPrincipal? principal) =>
    principal?.Identity?.IsAuthenticated ?? false;

  public static bool IsClaimsPrincipalWithScheme (ClaimsPrincipal? principal, string schemeName) =>
    principal?.Identity?.AuthenticationType == schemeName;

}