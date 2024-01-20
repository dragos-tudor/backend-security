using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static HttpResponse SanitizeChallengeHttpResponse(HttpResponse response)
  {
    if (IsEmptyString(response.Headers.Location))
      SetResponseLocation(response, "(not set)");

    if (IsEmptyString(response.Headers.SetCookie))
      SetResponseLocation(response, "(not set)");

    return response;
  }
}