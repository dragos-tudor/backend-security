
namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static string FormatOAuthScopes (OAuthOptions oAuthOptions) =>
    string.Join(oAuthOptions.ScopeSeparator, oAuthOptions.Scope!); // OAuth2 3.3 space separated

}