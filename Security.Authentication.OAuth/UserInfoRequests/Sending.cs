
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OAuth;

partial class Funcs {

  public static Task<HttpResponseMessage> SendUserInfoRequestAsync (HttpRequestMessage request, HttpClient remoteClient, CancellationToken cancellationToken) =>
    remoteClient.SendAsync(request, cancellationToken);

}