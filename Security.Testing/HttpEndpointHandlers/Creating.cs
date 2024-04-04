
namespace Security.Testing;

partial class Funcs {

  static HttpEndpointsHandler CreateHttpEndpointsHandler (HttpEndpoint[] endpoints) =>
    new (endpoints);

}