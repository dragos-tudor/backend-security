
namespace Security.Authentication;

partial class AuthenticationFuncs {

  public static bool IsPrincipalAuthenticated (ClaimsPrincipal? principal) =>
    GetPrincipalIdentity(principal)?.IsAuthenticated ?? false;

}