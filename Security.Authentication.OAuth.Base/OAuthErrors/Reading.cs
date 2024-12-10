
using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static async Task<OAuthError> ReadJsonOAuthError(HttpResponseMessage response, CancellationToken cancellationToken)
  {
    using var jsonContent = await ReadHttpResponseJsonContent(response, cancellationToken);
    return GetOAuthError(jsonContent.RootElement);
  }
}