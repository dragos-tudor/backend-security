
namespace Security.Testing;

partial class Funcs {

  public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string requestUri, params (string, string)[] headers)
  {
    using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
    SetRequestMessageHeaders(request, headers);
    return client.SendAsync(request);
  }

}
