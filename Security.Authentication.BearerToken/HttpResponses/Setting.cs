
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static HttpContext SetWWWAuthenticateResponseHeader(HttpContext context, string headerValue)
  {
    SetHttpResponseHeader(context, HeaderNames.WWWAuthenticate, headerValue);
    return context;
  }
}