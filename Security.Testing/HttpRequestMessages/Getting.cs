
namespace Security.Testing;

partial class Funcs {

  internal static string GetRequestPath(HttpRequestMessage request) =>
    request.RequestUri!.PathAndQuery.Split("?")[0];

  public static string? GetRequestMessageContent (HttpRequestMessage request) =>
    request.Content?.ReadAsStringAsync().Result;

}

