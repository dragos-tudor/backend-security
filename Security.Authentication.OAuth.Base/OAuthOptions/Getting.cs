
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string GetClaimsIssuer(OAuthOptions oauthOptions) => oauthOptions.ClaimsIssuer ?? oauthOptions.SchemeName;
}