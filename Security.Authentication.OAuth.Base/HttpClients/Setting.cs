
using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  const string AuthenticationScheme = "X-Authentication-Scheme";
  const string UserAgentHeader = "Security.Authentication.OAuth";
  const long MaxResponseBufferSize = 1024 * 1024 * 10; // 10 MB

  static void SetHttpClientAuthenticationScheme(HttpClient client, string schemeName) => client.DefaultRequestHeaders.Add(AuthenticationScheme, schemeName);

  static long SetHttpClientMaxResponseBufferSize(HttpClient client) => client.MaxResponseContentBufferSize = MaxResponseBufferSize;

  static void SetHttpClientUserAgent(HttpClient client) => client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgentHeader);

  static TimeSpan SetHttpClientTimeout(HttpClient client, TimeSpan timeout) => client.Timeout = timeout;

  public static HttpClient SetHttpClient(HttpClient client, string schemeName, TimeSpan? timeout = default)
  {
    SetHttpClientAuthenticationScheme(client, schemeName);
    SetHttpClientMaxResponseBufferSize(client);
    SetHttpClientTimeout(client, timeout ?? TimeSpan.FromSeconds(60));
    SetHttpClientUserAgent(client);
    return client;
  }
}