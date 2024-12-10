
using System.Text.Json;
using static System.Text.Json.JsonDocument;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static Task<string> ReadHttpResponseContent(HttpResponseMessage response, CancellationToken cancellationToken = default) => response.Content.ReadAsStringAsync(cancellationToken);

  public static async Task<JsonDocument> ReadHttpResponseJsonContent(HttpResponseMessage response, CancellationToken cancellationToken = default) => Parse(await ReadHttpResponseContent(response, cancellationToken));

  public static async Task<IDictionary<string, string>> ReadHttpResponseJsonProps(HttpResponseMessage response, CancellationToken cancellationToken = default)
  {
    using var jsonResponse = await ReadHttpResponseJsonContent(response, cancellationToken);
    return ToJsonPropsDictionary(jsonResponse.RootElement);
  }
}