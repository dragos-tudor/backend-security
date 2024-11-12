using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static HttpResponse SanitizeResponse(HttpResponse response)
  {
    if(IsEmptyString(response.Headers.Location)) SetResponseLocation(response, "(not set)");
    if(IsEmptyString(response.Headers.SetCookie)) SetResponseLocation(response, "(not set)");
    return response;
  }
}