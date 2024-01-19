
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static long? SetResponseContentLength (HttpResponse response, int length) =>
    response.Headers.ContentLength = length;

  static string? SetResponseContentType (HttpResponse response, string mimeType) =>
    response.Headers.ContentType = mimeType;

}