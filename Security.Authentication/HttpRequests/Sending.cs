
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static Task<HttpResponseMessage> SendHttpRequest(HttpRequestMessage request, HttpClient httpClient, CancellationToken cancellationToken = default) => httpClient.SendAsync(request, cancellationToken);
}