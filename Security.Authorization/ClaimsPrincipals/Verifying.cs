
using System.Security.Claims;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  static bool IsAuthenticatedClaimsPrincipal (ClaimsPrincipal? principal) =>
    principal?.Identity?.IsAuthenticated ?? false;

  public static bool IsClaimsPrincipalWithScheme (ClaimsPrincipal? principal, string schemeName) =>
    principal?.Identity?.AuthenticationType == schemeName;

}