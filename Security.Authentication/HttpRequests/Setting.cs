
using System.Net.Http.Headers;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static void SetHttpRequestAcceptType(HttpRequestMessage request, string mediaType) => request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

  public static void SetHttpRequestAuthorization(HttpRequestMessage request, string scheme, string value) => request.Headers.Authorization = new AuthenticationHeaderValue(scheme, value);

  public static HttpContent? SetHttpRequestContent(HttpRequestMessage request, IDictionary<string, string> @params) => request.Content = new FormUrlEncodedContent(@params);

  public static Version SetHttpRequestVersion(HttpRequestMessage request, Version version) => request.Version = version;
}