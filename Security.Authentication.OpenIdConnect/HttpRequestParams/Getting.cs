using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static KeyValuePair<string, string[]> GetRequestParam(KeyValuePair<string, StringValues> pair) => new (pair.Key, pair.Value.ToArray()!);

  static IEnumerable<KeyValuePair<string, string[]>> GetFormRequestParams(IFormCollection form) => form.Select(GetRequestParam);

  static IEnumerable<KeyValuePair<string, string[]>> GetQueryRequestParams(HttpRequest request) => request.Query.Select(GetRequestParam);

  static async Task<IEnumerable<KeyValuePair<string, string[]>>> GetRequestParams(HttpRequest request, CancellationToken cancellationToken = default) => request switch {
    var req when IsGetRequest(req) => GetQueryRequestParams(request),
    var req when IsFormPostRequest(req) => GetFormRequestParams(await ReadRequestForm(request, cancellationToken)),
    _ => []
  };
}