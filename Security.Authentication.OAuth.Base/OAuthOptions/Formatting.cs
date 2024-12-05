
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string FormatOAuthScopes(OAuthOptions oauthOptions) => string.Join(oauthOptions.ScopeSeparator, oauthOptions.Scope!); // OAuth2 3.3 space separated
}