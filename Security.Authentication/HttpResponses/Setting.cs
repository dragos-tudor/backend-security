
using System.Net;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static long? SetHttpResponseContentLength(HttpResponse response, int length) => response.Headers.ContentLength = length;

  public static string? SetHttpResponseContentType(HttpResponse response, string mimeType) => response.Headers.ContentType = mimeType;

  public static IHeaderDictionary SetHttpResponseHeader(HttpContext context, string headerName, string headerValue)
  {
    context.Response.Headers.Append(headerName, headerValue);
    return context.Response.Headers;
  }

  public static string? SetHttpResponseLocation(HttpResponse response, string location) => response.Headers.Location = location;

  public static string SetHttpResponseRedirect(HttpResponse response, string redirectUri) { response.Redirect(redirectUri); return redirectUri; }

  public static int SetHttpResponseStatus(HttpResponse response, HttpStatusCode statusCode) => response.StatusCode =(int)statusCode;
}