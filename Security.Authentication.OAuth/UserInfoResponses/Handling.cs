
using System.Net.Http;
using System.Threading;
using static System.Text.Json.JsonDocument;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<UserInfoResult> HandleUserInfoResponse(
    HttpResponseMessage response,
    OAuthOptions authOptions,
    CancellationToken cancellationToken = default)
  {
    using var userResponse = await ReadHttpResponseJsonResponse(response, cancellationToken);
    var userData = userResponse.RootElement;

    if (!IsSuccessHttpResponse(response)) return GetOAuthErrorType(userData);
    return MapOAuthOptionsClaims(authOptions, userData);
  }
}