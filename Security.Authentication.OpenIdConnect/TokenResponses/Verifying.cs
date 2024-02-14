using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsSuccessTokenResponse(HttpResponseMessage response) =>
    response.IsSuccessStatusCode;

  static bool IsJsonContentTypeTokenResponse(string contentType) =>
    string.Equals(contentType, "application/json", StringComparison.OrdinalIgnoreCase);
}