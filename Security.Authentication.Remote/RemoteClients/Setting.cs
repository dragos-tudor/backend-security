
using System.Net.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static HttpClient SetRemoteClient (
    HttpClient client,
    string schemeName,
    TimeSpan? timeout = default)
  {
    SetRemoteClientAuthenticationScheme(client, schemeName);
    SetRemoteClientMaxResponseBufferSize(client);
    SetRemoteClientTimeout(client, timeout ?? TimeSpan.FromSeconds(60));
    SetRemoteClientUserAgent(client);
    return client;
  }
}