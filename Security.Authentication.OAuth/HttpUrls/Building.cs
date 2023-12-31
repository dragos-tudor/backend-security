
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  const string RemoteError = "remote authentication error";

  static string BuildAccessDeniedErrorPath<TOptions>(
    TOptions authOptions,
    string errorMessage)
  where TOptions: OAuthOptions =>
    $"{authOptions.AccessDeniedPath}?error_description={WebUtility.UrlEncode(errorMessage)}";

  static string BuildChallengePath<TOptions> (
    TOptions authOptions,
    string returnUri)
  where TOptions: OAuthOptions =>
    authOptions.ChallengePath + QueryString.Create(authOptions.ReturnUrlParameter, returnUri);

  static string BuildGenericErrorPath<TOptions> (
    TOptions authOptions,
    string errorMessage)
  where TOptions: OAuthOptions =>
    $"{authOptions.ErrorPath}?error_name={WebUtility.UrlEncode(RemoteError)}&error_description={WebUtility.UrlEncode(errorMessage)}";

  static string BuildErrorPath<TOptions> (
    TOptions authOptions,
    Exception exception)
  where TOptions: OAuthOptions =>
    IsAccessDeniedError(exception.Message)?
      BuildAccessDeniedErrorPath(authOptions, exception.Message):
      BuildGenericErrorPath(authOptions, exception.Message);

}