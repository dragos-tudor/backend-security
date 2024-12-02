using System.Net.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static bool IsJsonContentTypeHttpResponse(string? contentType) => string.Equals(contentType, "application/json", StringComparison.OrdinalIgnoreCase);

  public static bool IsJwtContentTypeHttpResponse(string? contentType) => string.Equals(contentType, "application/jwt", StringComparison.OrdinalIgnoreCase);

  public static bool IsSuccessHttpResponse(HttpResponseMessage response) => response.IsSuccessStatusCode;
}