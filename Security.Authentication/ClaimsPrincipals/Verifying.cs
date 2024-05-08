
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static bool IsAuthenticatedPrincipal (ClaimsPrincipal? principal) =>
    GetPrincipalIdentity(principal)?.IsAuthenticated ?? false;
}