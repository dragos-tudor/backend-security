
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public const string AccessDeniedError = "access was denied by the resource owner or by the remote server";
  public const string AccessDeniedToken = "access_denied";
  public const string ErrorKey = "error";

  public static bool IsAccessDeniedError (HttpRequest request, string errorKey = ErrorKey) =>
    request.Query[errorKey] == AccessDeniedToken;

  public static bool IsAccessDeniedError (string? error) =>
    error == AccessDeniedError;

  public static bool IsGenericError (HttpRequest request) =>
    IsNotEmptyString(request.Query[ErrorKey]);
}