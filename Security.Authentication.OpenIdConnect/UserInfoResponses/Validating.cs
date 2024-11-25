using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? ValidateUserInfoResponse(HttpResponseMessage response)
  {
    var contentType = GetHttpResponseContentType(response);
    var statusCode = GetHttpResponseStatusCode(response);

    response.EnsureSuccessStatusCode();
    if (IsEmptyString(contentType)) return $"Unexpected user info response format. Content-Type header is missing. Status Code: {statusCode}";
    if (!IsValidContentTypeHttpResponse(contentType!)) return $"Unexpected user info response format. Content-Type {contentType}. Status Code: {statusCode}.";
    return default;
  }
}