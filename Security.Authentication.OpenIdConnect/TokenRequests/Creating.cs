
using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs {

  static HttpRequestMessage CreateTokenRequest (string endpoint) =>
    new (HttpMethod.Post, endpoint);

}