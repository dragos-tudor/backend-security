
using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static HttpRequestMessage CreateUserInfoRequest (string requestUri) =>
    new (HttpMethod.Get, requestUri);

}