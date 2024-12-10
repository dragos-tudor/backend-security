
using System.Net.Http;
using System.Net.Mime;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static HttpRequestMessage SetTokenRequest(
    HttpRequestMessage request,
    OAuthParams tokenParams,
    Version requestVersion)
  {
    SetHttpRequestAcceptType(request, MediaTypeNames.Application.Json);
    SetHttpRequestVersion(request, requestVersion);
    SetHttpRequestContent(request, tokenParams);
    return request;
  }
}