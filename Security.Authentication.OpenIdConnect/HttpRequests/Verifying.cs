using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string FormMimeType = "application/x-www-form-urlencoded";

  static bool IsFormContentTypeRequest(HttpRequest request) =>
    (request.ContentType ?? string.Empty)
      .StartsWith(FormMimeType, StringComparison.OrdinalIgnoreCase);

  static bool IsFormPostRequest(HttpRequest request) =>
    IsPostRequest(request) &&
    IsFormContentTypeRequest(request) &&
    IsReadableBodyRequest(request);

  static bool IsGetRequest(HttpRequest request) => HttpMethods.IsGet(request.Method);

  static bool IsPostRequest(HttpRequest request) => HttpMethods.IsPost(request.Method);

  static bool IsReadableBodyRequest(HttpRequest request) => request.Body.CanRead;
}