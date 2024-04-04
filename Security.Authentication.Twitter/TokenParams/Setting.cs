
using System.Net.Http;
using System.Net.Http.Headers;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs {

  static AuthenticationHeaderValue SetAuthorizationHeader (HttpRequestMessage request, string scheme, string credentials) =>
    request.Headers.Authorization = new AuthenticationHeaderValue(scheme, credentials);

}