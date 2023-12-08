
using System.Security.Principal;

namespace Security.Authentication;

partial class Funcs {

  public static IIdentity? GetPrincipalIdentity (ClaimsPrincipal? principal) =>
    principal?.Identity;

  public static string? GetPrincipalName (ClaimsPrincipal? principal) =>
    principal?.Identity?.Name;

  public static string? GetPrincipalNameId (ClaimsPrincipal? principal) =>
    GetNameIdClaim(principal?.Claims)?.Value;

}