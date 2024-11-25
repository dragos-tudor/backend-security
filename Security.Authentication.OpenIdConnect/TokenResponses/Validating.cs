using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? ValidateTokenResponse(HttpResponseMessage response)
  {
    var contentType = GetHttpResponseContentType(response);
    var statusCode = GetHttpResponseStatusCode(response);

    if (IsEmptyString(contentType)) return $"Unexpected token response format. Content-Type header is missing. Status Code: {statusCode}";
    if (!IsJsonContentTypeHttpResponse(contentType!)) return $"Unexpected token response format. Content-Type {contentType}. Status Code: {statusCode}.";
    return default;
  }
}