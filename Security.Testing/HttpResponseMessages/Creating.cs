
namespace Security.Testing;

partial class Funcs {

  public static HttpResponseMessage CreateResponseMessage(HttpContent content, HttpStatusCode statusCode = HttpStatusCode.OK) =>
    new () {Content = content, StatusCode = statusCode};
}