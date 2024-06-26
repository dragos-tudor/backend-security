
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static Task<HttpResponseMessage> SendUserInfoRequest (
    HttpRequestMessage request,
    HttpClient httpClient,
    CancellationToken cancellationToken = default
  ) =>
    httpClient.SendAsync(request, cancellationToken);

}