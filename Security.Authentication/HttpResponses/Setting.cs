
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static long? SetResponseContentLength (HttpResponse response, int length) => response.Headers.ContentLength = length;

  public static string? SetResponseContentType (HttpResponse response, string mimeType) => response.Headers.ContentType = mimeType;

  public static IHeaderDictionary SetResponseHeader(HttpContext context, string headerName, string headerValue)
  {
    context.Response.Headers.Append(headerName, headerValue);
    return context.Response.Headers;
  }

  public static string? SetResponseLocation (HttpResponse response, string location) => response.Headers.Location = location;

  public static string SetResponseRedirect (HttpResponse response, string redirectUri) { response.Redirect(redirectUri); return redirectUri; }

  public static string? SetResponseSetCookie (HttpResponse response, string cookie) => response.Headers.SetCookie = cookie;

  public static int SetResponseStatus(HttpContext context, HttpStatusCode statusCode) => context.Response.StatusCode = (int)statusCode;
}