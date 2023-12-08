
using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class Funcs {

  static HttpRequestMessage CreateUserInfoRequest (string requestUri) =>
    new (HttpMethod.Get, requestUri);

}