
using System.Net.Http;
using System.Net.Http.Headers;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static void SetUserInfoRequestBearer(HttpRequestMessage request, string accessTokenHeader, string accessToken) =>
    request.Headers.Authorization = new AuthenticationHeaderValue(accessTokenHeader, accessToken);

  static void SetUserInfoRequestAcceptType(HttpRequestMessage request, string mediaType) =>
    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

  static Version SetUserInfoRequestVersion (HttpRequestMessage request, Version version) =>
    request.Version = version;
}