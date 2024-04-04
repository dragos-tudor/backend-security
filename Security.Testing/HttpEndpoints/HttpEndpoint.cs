
namespace Security.Testing;

public sealed record HttpEndpoint(string Route, Func<HttpRequestMessage, HttpResponseMessage> EndpointHandler);