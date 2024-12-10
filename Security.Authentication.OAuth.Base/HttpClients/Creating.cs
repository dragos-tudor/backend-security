
#pragma warning disable CA2000

using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static TimeSpan DefaultPooledConnectionLifetime = TimeSpan.FromMinutes(2);

  static SocketsHttpHandler CreateSocketsHttpHandler(TimeSpan? connectionLifetime) => new() { PooledConnectionLifetime = connectionLifetime ?? DefaultPooledConnectionLifetime };

  public static HttpClient CreateHttpClient(HttpMessageHandler? messageHandler = default, TimeSpan? connectionLifetime = default) => new(messageHandler ?? CreateSocketsHttpHandler(connectionLifetime));
}