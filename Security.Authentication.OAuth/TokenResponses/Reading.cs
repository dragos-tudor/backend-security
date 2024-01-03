
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using static System.Text.Json.JsonDocument;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static Task<string> ReadTokenResponseContent (HttpResponseMessage response, CancellationToken cancellationToken = default) =>
    response.Content.ReadAsStringAsync(cancellationToken);

  public static async Task<JsonDocument> ReadTokenResponse (HttpResponseMessage response, CancellationToken cancellationToken = default) =>
    Parse(await ReadTokenResponseContent(response, cancellationToken));

}