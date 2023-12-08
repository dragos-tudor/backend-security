
namespace Security.Testing;

partial class Funcs {

  static Dictionary<string, Func<HttpRequestMessage, HttpResponseMessage>> MapEndpoint(
    Dictionary<string, Func<HttpRequestMessage, HttpResponseMessage>> endpoints,
    HttpEndpoint endpoint)
  {
    endpoints.Add(endpoint.Route, (request) => CreateResponseMessage(request, endpoint));
    return endpoints;
  }

}