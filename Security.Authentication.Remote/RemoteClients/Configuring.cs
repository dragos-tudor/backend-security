
using System.Net.Http;

namespace Security.Authentication.Remote;

partial class Funcs {

  public static HttpClient ConfigureRemoteClient (HttpClient client, string schemeName, TimeSpan? timeout = default)
  {
    SetRemoteClientAthenticationScheme(client, schemeName);
    SetRemoteClientMaxResponseBufferSize(client);
    SetRemoteClientTimeout(client, timeout ?? TimeSpan.FromSeconds(60));
    SetRemoteClientUserAgent(client);
    return client;
  }

}