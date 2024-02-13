
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal const string AccessDeniedError = "access was denied by the resource owner or by the remote server";
  internal const string AccessDeniedToken = "access_denied";
  internal const string ErrorKey = "error";

  static bool IsAccessDeniedError (HttpRequest request, string errorKey = ErrorKey) =>
    request.Query[errorKey] == AccessDeniedToken;

  public static bool IsAccessDeniedError (string? error) =>
    error == AccessDeniedError;

  static bool IsGenericError (HttpRequest request) =>
    IsNotEmptyString(request.Query[ErrorKey]);
}