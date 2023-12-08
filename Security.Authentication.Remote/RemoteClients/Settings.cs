
using System.Net.Http;

namespace Security.Authentication.Remote;

partial class Funcs {

  const string AuthenticationScheme = "X_Authentication-Scheme";
  const long MaxResponseBufferSize = 1024 * 1024 * 10; // 10 MB
  const string UserAgentHeader = "Security Authentication handler";

  static void SetRemoteClientAthenticationScheme (HttpClient client, string schemeName) =>
    client.DefaultRequestHeaders.Add(AuthenticationScheme, schemeName);

  static long SetRemoteClientMaxResponseBufferSize (HttpClient client) =>
    client.MaxResponseContentBufferSize = MaxResponseBufferSize;

  static void SetRemoteClientUserAgent (HttpClient client) =>
    client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgentHeader);

  static TimeSpan SetRemoteClientTimeout (HttpClient client, TimeSpan timeout) =>
    client.Timeout = timeout;

}