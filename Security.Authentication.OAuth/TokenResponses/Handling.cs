
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Microsoft.AspNetCore.Http;
using static System.Text.Json.JsonDocument;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal const string AccessTokenNotFound = "failed to retrieve oauth access token";

  public static async Task<TokenResult> HandleTokenResponse(
    HttpResponseMessage response,
    CancellationToken cancellationToken = default)
  {
    using var tokenResponse = await ReadHttpResponseJsonContent(response, cancellationToken);
    var tokenData = tokenResponse.RootElement;

    if (!IsSuccessHttpResponse(response)) return GetOAuthErrorType(tokenData);
    if (!ExistsAccessToken(tokenData)) return AccessTokenNotFound;

    return CreateOAuthTokens(tokenData);
  }
}