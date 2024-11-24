
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string FormatOAuthScopes(OAuthOptions authOptions) => string.Join(authOptions.ScopeSeparator, authOptions.Scope!); // OAuth2 3.3 space separated
}