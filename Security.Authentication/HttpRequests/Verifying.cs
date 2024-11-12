using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  const string FormMimeType = "application/x-www-form-urlencoded";

  static bool IsFormContentTypeRequest(HttpRequest request) =>(request.ContentType ?? string.Empty).StartsWith(FormMimeType, StringComparison.OrdinalIgnoreCase);

  public static bool IsFormPostRequest(HttpRequest request) => IsPostRequest(request) && IsFormContentTypeRequest(request) && IsReadableBodyRequest(request);

  public static bool IsGetRequest(HttpRequest request) => HttpMethods.IsGet(request.Method);

  public static bool IsPostRequest(HttpRequest request) => HttpMethods.IsPost(request.Method);

  public static bool IsReadableBodyRequest(HttpRequest request) => request.Body.CanRead;
}