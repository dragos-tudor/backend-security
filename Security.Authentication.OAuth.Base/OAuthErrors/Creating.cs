
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static OAuthError CreateOAuthError(string errorType, string? errorDescription = default, string? errorUri = default) => new (errorType, errorDescription, errorUri);
}
