using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static IEnumerable<KeyValuePair<string, string[]>> GetFormPostAuthorizationParams(IFormCollection form) =>
    form.Select(SkipPostAuthorizationParam);

  static IEnumerable<KeyValuePair<string, string[]>> GetQueryPostAuthorizationParams(HttpRequest request) =>
    request.Query.Select(SkipPostAuthorizationParam);

  static async Task<IEnumerable<KeyValuePair<string, string[]>>?> GetPostAuthorizationParams(HttpRequest request, CancellationToken cancellationToken = default)
  {
    if(IsGetRequest(request)) return GetQueryPostAuthorizationParams(request);
    if(IsFormPostRequest(request)) return GetFormPostAuthorizationParams(await ReadRequestForm(request, cancellationToken));
    return default;
  }
}