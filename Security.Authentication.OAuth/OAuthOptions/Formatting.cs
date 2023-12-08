
namespace Security.Authentication.OAuth;

partial class Funcs {

  public static string FormatOAuthScopes (OAuthOptions options) =>
    string.Join(options.ScopeSeparator, options.Scope); // OAuth2 3.3 space separated

}