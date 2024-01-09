using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static IHeaderDictionary SetResponseHeader(HttpContext context, string headerName, string headerValue)
  {
    context.Response.Headers.Append(headerName, headerValue);
    return context.Response.Headers;
  }

  static int SetResponseStatus(HttpContext context, HttpStatusCode statusCode) =>
    context.Response.StatusCode = (int)statusCode;
}