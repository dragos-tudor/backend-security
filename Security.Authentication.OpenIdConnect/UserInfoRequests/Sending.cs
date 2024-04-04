
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static Task<HttpResponseMessage> SendUserInfoRequest (HttpRequestMessage request, HttpClient httpClient, CancellationToken cancellationToken = default) =>
    httpClient.SendAsync(request, cancellationToken);
}