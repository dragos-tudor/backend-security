
namespace Security.Authentication;

partial class Funcs {

  public static string GetClaimsIssuer (AuthenticationOptions authOptions) =>
    authOptions.ClaimsIssuer ?? authOptions.SchemeName;

}