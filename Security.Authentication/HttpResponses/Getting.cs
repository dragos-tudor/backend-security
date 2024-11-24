
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? GetHttpResponseContentType(HttpResponseMessage response) => response.Headers.GetValues(HeaderNames.ContentType)?.FirstOrDefault();

  public static string? GetHttpResponseLocation(HttpResponse response) => response.Headers.Location;

  public static string? GetHttpResponseSetCookie(HttpResponse response) => response.Headers.SetCookie;

  public static int GetHttpResponseStatusCode(HttpResponseMessage response) => (int)response.StatusCode;
}