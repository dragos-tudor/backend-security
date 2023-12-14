
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static Task<string> ReadUserInfoResponseContentAsync (HttpResponseMessage response, CancellationToken cancellationToken = default) =>
    response.Content.ReadAsStringAsync(cancellationToken);

}