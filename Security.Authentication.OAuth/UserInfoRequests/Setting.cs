
using System.Net.Http;
using System.Net.Mime;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  const string Bearer = "Bearer";

  public static HttpRequestMessage SetUserInfoRequest(
    HttpRequestMessage request,
    string accessToken)
  {
    SetHttpRequestAcceptType(request, MediaTypeNames.Application.Json);
    SetHttpRequestAuthorization(request, Bearer, accessToken);
    return request;
  }
}