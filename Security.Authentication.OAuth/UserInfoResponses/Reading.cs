
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OAuth;

partial class Funcs {

  static Task<string> ReadUserInfoResponseContentAsync (HttpResponseMessage response, CancellationToken cancellationToken) =>
    response.Content.ReadAsStringAsync(cancellationToken);

}