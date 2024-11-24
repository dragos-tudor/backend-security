
using System.Net.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static HttpRequestMessage CreateHttpGetRequest(string endpoint) => new(HttpMethod.Get, endpoint);

  public static HttpRequestMessage CreateHttpPostRequest(string endpoint) => new(HttpMethod.Post, endpoint);
}