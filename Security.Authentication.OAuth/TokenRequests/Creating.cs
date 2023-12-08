
using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class Funcs {

  static HttpRequestMessage CreateTokenRequest (string endpoint) =>
    new (HttpMethod.Post, endpoint);

}