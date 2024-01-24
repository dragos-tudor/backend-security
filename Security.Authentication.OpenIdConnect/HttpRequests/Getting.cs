using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static KeyValuePair<string, string[]> SkipEmptyKeyValues(KeyValuePair<string, StringValues> pair) =>
    new (pair.Key, pair.Value.ToArray()!);

  static IEnumerable<KeyValuePair<string, string[]>> GetRequestFormParams(IFormCollection form) =>
    form.Select(SkipEmptyKeyValues);

  static IEnumerable<KeyValuePair<string, string[]>> GetRequestQueryParams(HttpRequest request) =>
    request.Query.Select(SkipEmptyKeyValues);

  static async Task<IEnumerable<KeyValuePair<string, string[]>>?> GetRequestParams(HttpRequest request, CancellationToken token = default)
  {
    if(IsGetRequest(request)) return GetRequestQueryParams(request);
    if(IsFormPostRequest(request)) return GetRequestFormParams(await ReadRequestForm(request, token));
    return default;
  }
}