
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string GetClaimsIssuer(OAuthOptions authOptions) => authOptions.ClaimsIssuer ?? authOptions.SchemeName;
}