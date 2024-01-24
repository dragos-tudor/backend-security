
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static void SetTokenRequestAcceptType (HttpRequestMessage request, string mediaType) =>
    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

  static HttpContent? SetTokenRequestContent (HttpRequestMessage request, IDictionary<string, string> tokenParams) =>
    request.Content = new FormUrlEncodedContent(tokenParams);

  static Version SetTokenRequestVersion (HttpRequestMessage request, Version version) =>
    request.Version = version;

}