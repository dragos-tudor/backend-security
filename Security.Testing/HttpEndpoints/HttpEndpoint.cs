
namespace Security.Testing;

public sealed record HttpEndpoint(string Route, Func<HttpRequestMessage, HttpResponseMessage> ResponseFunc, int StatusCode = 200);