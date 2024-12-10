
using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal const string AccessTokenNotFound = "failed to retrieve oauth access token";

  public static async Task<TokenResult> HandleTokenResponse(
    HttpResponseMessage response,
    CancellationToken cancellationToken = default)
  {
    if (!IsSuccessHttpResponse(response)) return await ReadJsonOAuthError(response, cancellationToken);

    var oauthTokens = await ReadJsonOAuthTokens(response, cancellationToken);
    if (!ExistsAccessToken(oauthTokens)) return AccessTokenNotFound;

    return oauthTokens;
  }
}