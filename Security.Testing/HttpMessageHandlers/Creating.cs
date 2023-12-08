
using System.Linq;

namespace Security.Testing;

partial class Funcs {

  static EndpointsMessageHandler CreateHttpMessageHandler (HttpEndpoint[] endpoints) =>
    new (endpoints.Aggregate(new Dictionary<string, Func<HttpRequestMessage, HttpResponseMessage>>(), MapEndpoint));

}