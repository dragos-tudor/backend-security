
namespace Security.Authentication;

partial class Funcs {

  public static bool IsPrincipalAuthenticated (ClaimsPrincipal? principal) =>
    GetPrincipalIdentity(principal)?.IsAuthenticated ?? false;

}