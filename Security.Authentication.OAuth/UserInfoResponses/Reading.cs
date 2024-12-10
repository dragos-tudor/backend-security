
using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<IDictionary<string, string>> ReadUserInfoClaims(HttpResponseMessage response, CancellationToken cancellationToken = default)
  {
    using var userInfoResponse = await ReadHttpResponseJsonContent(response, cancellationToken);
    return ToJsonPropsDictionary(userInfoResponse.RootElement);
  }
}