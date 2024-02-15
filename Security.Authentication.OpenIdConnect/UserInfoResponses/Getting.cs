using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string? GetUserInfoResponseContentType(HttpResponseMessage response) =>
    response.Content.Headers.ContentType?.MediaType;

  static int GetUserInfoResponseStatusCode(HttpResponseMessage response) =>
    (int)response.StatusCode;
}