
using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static async Task<OAuthTokens> ReadJsonOAuthTokens(HttpResponseMessage response, CancellationToken cancellationToken)
  {
    using var tokenResponse = await ReadHttpResponseJsonContent(response, cancellationToken);
    return CreateOAuthTokens(tokenResponse.RootElement);
  }
}