
namespace Security.Testing;
#pragma warning disable CA2000 // Dispose objects before losing scope

partial class Funcs
{
  public static HttpClient CreateHttpClient(string baseAddress, params HttpEndpoint[] endpoints) =>
    new(CreateHttpEndpointsHandler(endpoints)) { BaseAddress = new Uri(baseAddress) };

  public static HttpClient CreateHttpClient(string baseAddress, string route, HttpContent content, int statusCode = 200) =>
    CreateHttpClient(baseAddress, new HttpEndpoint(route, (_) => CreateResponseMessage(content, (HttpStatusCode)statusCode)));

  public static HttpClient CreateHttpClient(string baseAddress, string route, Func<HttpRequestMessage, HttpContent> contentFunc, int statusCode = 200) =>
    CreateHttpClient(baseAddress, new HttpEndpoint(route, (request) => CreateResponseMessage(contentFunc(request), (HttpStatusCode)statusCode)));
}