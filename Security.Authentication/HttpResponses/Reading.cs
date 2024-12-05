
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static System.Text.Json.JsonDocument;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static Task<string> ReadHttpResponseContent(HttpResponseMessage response, CancellationToken cancellationToken = default) => response.Content.ReadAsStringAsync(cancellationToken);

  public static async Task<JsonDocument> ReadHttpResponseJsonContent(HttpResponseMessage response, CancellationToken cancellationToken = default) => Parse(await ReadHttpResponseContent(response, cancellationToken));
}