using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  const string FormMimeType = "application/x-www-form-urlencoded";

  static bool IsFormContentTypeHttpRequest(HttpRequest request) =>(request.ContentType ?? string.Empty).StartsWith(FormMimeType, StringComparison.OrdinalIgnoreCase);

  public static bool IsFormPostHttpRequest(HttpRequest request) => IsPostHttpRequest(request) && IsFormContentTypeHttpRequest(request) && IsReadableBodyHttpRequest(request);

  public static bool IsGetHttpRequest(HttpRequest request) => HttpMethods.IsGet(request.Method);

  public static bool IsPostHttpRequest(HttpRequest request) => HttpMethods.IsPost(request.Method);

  public static bool IsReadableBodyHttpRequest(HttpRequest request) => request.Body.CanRead;
}