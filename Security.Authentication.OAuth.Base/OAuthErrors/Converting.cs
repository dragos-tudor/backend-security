
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static string? ToOAuthErrorQueryDescription(OAuthError error) => IsNotEmptyString(error.ErrorDescription)? $"&{ErrorDescriptionToken}={error.ErrorDescription}": default;

  static string ToOAuthErrorQueryType(OAuthError error) => $"{ErrorTypeToken}={error.ErrorType}";

  static string? ToOAuthErrorQueryUri(OAuthError error) => IsNotEmptyString(error.ErrorUri)? $"&{ErrorUriToken}={error.ErrorUri}": default;

  public static string ToOAuthErrorString(OAuthError error) => $"error type: {error.ErrorType}\nerror description: {error.ErrorDescription}\nerror uri: {error.ErrorUri}";

  public static string ToOAuthErrorQuery(OAuthError error) => $"{ToOAuthErrorQueryType(error)}{ToOAuthErrorQueryDescription(error)}{ToOAuthErrorQueryUri(error)}";
}