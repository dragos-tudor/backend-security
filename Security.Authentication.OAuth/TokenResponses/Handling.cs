
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Microsoft.AspNetCore.Http;
using static System.Text.Json.JsonDocument;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal const string AccessTokenNotFound = "failed to retrieve access token";

  public static async Task<TokenResult> HandleTokenResponse(
    HttpResponseMessage response,
    CancellationToken cancellationToken = default)
  {
    var responseContent = await ReadHttpResponseContent(response, cancellationToken);
    using var tokenResponse = Parse(responseContent);
    var tokenElement = tokenResponse.RootElement;

    if(!IsSuccessHttpResponse(response)) return GetOAuthErrorType(tokenElement);
    if(!ExistsAccessToken(tokenElement)) return AccessTokenNotFound;

    return CreateTokenInfo(tokenElement);
  }
}