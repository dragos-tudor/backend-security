
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using static System.Text.Json.JsonDocument;

namespace Security.Authentication.OAuth;

partial class Funcs {

  static Task<string> ReadTokenResponseContentAsync (HttpResponseMessage response, CancellationToken cancellationToken) =>
    response.Content.ReadAsStringAsync(cancellationToken);

  public static async Task<JsonDocument> ReadTokenResponseAsync (HttpResponseMessage response, CancellationToken cancellationToken) =>
    Parse(await ReadTokenResponseContentAsync(response, cancellationToken));

}