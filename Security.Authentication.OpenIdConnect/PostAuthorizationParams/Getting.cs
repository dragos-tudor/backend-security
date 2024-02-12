using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static IEnumerable<KeyValuePair<string, string[]>> GetFormPostAuthorizeParams(IFormCollection form) =>
    form.Select(SkipPostAuthorizeParam);

  static IEnumerable<KeyValuePair<string, string[]>> GetQueryPostAuthorizeParams(HttpRequest request) =>
    request.Query.Select(SkipPostAuthorizeParam);

  static async Task<IEnumerable<KeyValuePair<string, string[]>>?> GetPostAuthorizeParams(HttpRequest request, CancellationToken cancellationToken = default)
  {
    if(IsGetRequest(request)) return GetQueryPostAuthorizeParams(request);
    if(IsFormPostRequest(request)) return GetFormPostAuthorizeParams(await ReadRequestForm(request, cancellationToken));
    return default;
  }
}