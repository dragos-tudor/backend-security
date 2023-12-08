
using System.Net.Http;

namespace Security.Authentication.Remote;

partial class Funcs {

  static SocketsHttpHandler CreateHttpMessageHandler(TimeSpan? connectionLifetime) =>
    new() { PooledConnectionLifetime = connectionLifetime ?? GetRemoteClientConnectionLifetime() };

  public static HttpClient CreateRemoteClient(HttpMessageHandler? messageHandler = default, TimeSpan? connectionLifetime = default) =>
    new(messageHandler ?? CreateHttpMessageHandler(connectionLifetime));

}