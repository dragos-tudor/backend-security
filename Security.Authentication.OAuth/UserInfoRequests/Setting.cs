
using System.Net.Http;
using System.Net.Http.Headers;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static void SetUserInfoRequestBearer(HttpRequestMessage request, string accessTokenHeader, string accessToken) =>
    request.Headers.Authorization = new AuthenticationHeaderValue(accessTokenHeader, accessToken);

  static void SetUserInfoRequestAcceptType(HttpRequestMessage request, string mediaType) =>
    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
}