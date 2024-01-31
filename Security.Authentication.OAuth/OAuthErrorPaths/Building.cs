
using System.Net;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  const string OAuthRemoteError = "oauth2 remote error";

  static string BuildAccessDeniedErrorPath<TOptions> (
    TOptions authOptions,
    string errorMessage)
  where TOptions: OAuthOptions =>
    $"{authOptions.AccessDeniedPath}?error_description={WebUtility.UrlEncode(errorMessage)}";

  static string BuildGenericErrorPath<TOptions> (
    TOptions authOptions,
    string errorMessage)
  where TOptions: OAuthOptions =>
    $"{authOptions.ErrorPath}?error_name={WebUtility.UrlEncode(OAuthRemoteError)}&error_description={WebUtility.UrlEncode(errorMessage)}";

  static string BuildErrorPath<TOptions> (
    TOptions authOptions,
    Exception exception)
  where TOptions: OAuthOptions =>
    IsAccessDeniedError(exception.Message)?
      BuildAccessDeniedErrorPath(authOptions, exception.Message):
      BuildGenericErrorPath(authOptions, exception.Message);

}