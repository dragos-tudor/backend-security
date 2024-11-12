
using Microsoft.AspNetCore.Http;

namespace Security.Testing;

partial class Funcs
{
  public static Task<HttpResponseMessage> PostAsync(this HttpClient client, PathString requestUri, params(string, string)[] headers)
  {
    using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
    SetRequestMessageHeaders(request, headers);
    return client.SendAsync(request);
  }

  public static Task<HttpResponseMessage> PostAsync(this HttpClient client, PathString requestUri, HttpContent content, params(string, string)[] headers)
  {
    using var request = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = content };
    SetRequestMessageHeaders(request, headers);
    return client.SendAsync(request);
  }
}

