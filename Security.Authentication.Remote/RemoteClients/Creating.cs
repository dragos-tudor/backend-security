
using System.Net.Http;

namespace Security.Authentication.Remote;
#pragma warning disable CA2000

partial class RemoteFuncs {

  static TimeSpan DefaultPooledConnectionLifetime = TimeSpan.FromMinutes(2);

  static SocketsHttpHandler CreateSocketsHttpHandler(TimeSpan? connectionLifetime) =>
    new() { PooledConnectionLifetime = connectionLifetime ?? DefaultPooledConnectionLifetime };

  public static HttpClient CreateRemoteClient(HttpMessageHandler? messageHandler = default, TimeSpan? connectionLifetime = default) =>
    new(messageHandler ?? CreateSocketsHttpHandler(connectionLifetime));

}