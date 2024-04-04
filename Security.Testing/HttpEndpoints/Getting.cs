using System.Linq;

namespace Security.Testing;

partial class Funcs
{
  internal static HttpEndpoint? GetHttpEndpoint (IEnumerable<HttpEndpoint> endpoints, string route) =>
    endpoints.FirstOrDefault(endpoint => endpoint.Route == route);
}