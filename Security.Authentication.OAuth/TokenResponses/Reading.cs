
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using static System.Text.Json.JsonDocument;

namespace Security.Authentication.OAuth;

partial class Funcs {

  static Task<string> ReadTokenResponseContentAsync (HttpResponseMessage response, CancellationToken cancellationToken = default) =>
    response.Content.ReadAsStringAsync(cancellationToken);

  public static async Task<JsonDocument> ReadTokenResponseAsync (HttpResponseMessage response, CancellationToken cancellationToken = default) =>
    Parse(await ReadTokenResponseContentAsync(response, cancellationToken));

}