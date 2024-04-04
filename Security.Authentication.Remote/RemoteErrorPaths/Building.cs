
using System.Net;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  const string RemoteError = "remote error";

  static string BuildAccessDeniedErrorPath<TOptions> (
    TOptions authOptions,
    string errorMessage)
  where TOptions: RemoteAuthenticationOptions =>
    $"{authOptions.AccessDeniedPath}?error_description={WebUtility.UrlEncode(errorMessage)}";

  static string BuildGenericErrorPath<TOptions> (
    TOptions authOptions,
    string errorMessage)
  where TOptions: RemoteAuthenticationOptions =>
    $"{authOptions.ErrorPath}?error_name={WebUtility.UrlEncode(RemoteError)}&error_description={WebUtility.UrlEncode(errorMessage)}";

  public static string BuildErrorPath<TOptions> (
    TOptions authOptions,
    Exception exception)
  where TOptions: RemoteAuthenticationOptions =>
    IsAccessDeniedError(exception.Message)?
      BuildAccessDeniedErrorPath(authOptions, exception.Message):
      BuildGenericErrorPath(authOptions, exception.Message);

}