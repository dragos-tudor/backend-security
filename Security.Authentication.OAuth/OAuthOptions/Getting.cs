
namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static string GetClaimsIssuer (OAuthOptions authOptions) =>
    authOptions.ClaimsIssuer ?? authOptions.SchemeName;

}