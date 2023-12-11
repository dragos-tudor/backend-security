
using System.Threading;
using static Security.Testing.Funcs;
#pragma warning disable CA1854

namespace Security.Testing;

public class EndpointsMessageHandler : HttpMessageHandler {

  readonly IDictionary<string, Func<HttpRequestMessage, HttpResponseMessage>> Endpoints = new Dictionary<string, Func<HttpRequestMessage, HttpResponseMessage>>();

  public EndpointsMessageHandler(IDictionary<string, Func<HttpRequestMessage, HttpResponseMessage>> endpoints) =>
    Endpoints = endpoints;

  protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default) =>
    Endpoints.ContainsKey(GetRequestPath(request)) ?
      Task.FromResult(Endpoints[GetRequestPath(request)](request)) :
      throw new Exception($"Endpoint route '{GetRequestPath(request)}' not found.");

}