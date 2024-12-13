
using System.Net.Http.Headers;

namespace Security.Testing;

partial class Funcs
{
  public static AuthenticationHeaderValue SetHttpClientAuthorizationHeader(HttpClient client, string scheme, string value) => client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, value);
}