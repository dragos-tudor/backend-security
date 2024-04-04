
using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static HttpRequestMessage CreateUserInfoRequest (string requestUri) =>
    new (HttpMethod.Get, requestUri);
}