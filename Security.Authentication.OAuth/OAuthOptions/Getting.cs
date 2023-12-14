
namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static string GetClaimsIssuer (OAuthOptions oAuthOptions) =>
    oAuthOptions.ClaimsIssuer ?? oAuthOptions.SchemeName;

}