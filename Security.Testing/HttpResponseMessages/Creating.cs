
using System.Net.Http.Json;

namespace Security.Testing;

partial class Funcs {

  static HttpResponseMessage CreateResponseMessage(HttpRequestMessage request, HttpEndpoint endpoint) =>
    endpoint.ResponseFunc(request);

  public static HttpResponseMessage CreateResponseMessage(HttpContent content, HttpStatusCode statusCode = HttpStatusCode.OK) =>
    new () {Content = content, StatusCode = statusCode};

  public static HttpResponseMessage CreateJsonResponseMessage<T>(T content, HttpStatusCode statusCode = HttpStatusCode.OK) =>
    new () {Content = JsonContent.Create(content), StatusCode = statusCode};

}