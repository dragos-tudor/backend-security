using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? GetTokenResponseContentType(HttpResponseMessage response) =>
    response.Content.Headers.ContentType?.MediaType;

  static int GetTokenResponseStatusCode(HttpResponseMessage response) =>
    (int)response.StatusCode;
}