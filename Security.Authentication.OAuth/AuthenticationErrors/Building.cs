
using System.Net;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  const string RemoteError = "remote authentication error";

  static string BuildAuthenticationAccessDeniedErrorPath (string errorMessage) =>
    $"/accessdenied?error_description={WebUtility.UrlEncode(errorMessage)}";

  static string BuildAuthenticationGenericErrorPath (string errorMessage) =>
    $"/error?error_name={WebUtility.UrlEncode(RemoteError)}&error_description={WebUtility.UrlEncode(errorMessage)}";

  static string BuildAuthenticationErrorPath (Exception exception) =>
    IsAccessDeniedError(exception.Message)?
      BuildAuthenticationAccessDeniedErrorPath(exception.Message):
      BuildAuthenticationGenericErrorPath(exception.Message);

}