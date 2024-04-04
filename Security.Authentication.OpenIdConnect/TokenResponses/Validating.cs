using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? ValidateTokenResponse(HttpResponseMessage response)
  {
    var contentType = GetTokenResponseContentType(response);
    var statusCode = GetTokenResponseStatusCode(response);

    if (IsEmptyString(contentType)) return $"Unexpected token response format. Content-Type header is missing. Status Code: {statusCode}";
    if (!IsJsonContentTypeTokenResponse(contentType!)) return $"Unexpected token response format. Content-Type {contentType}. Status Code: {statusCode}.";
    return default;
  }
}