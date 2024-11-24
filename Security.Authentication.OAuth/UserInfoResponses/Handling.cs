
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
    var responseContent = await ReadHttpResponseContent(response, cancellationToken);
    using var userInfoResponse = Parse(responseContent);
    var userData = userInfoResponse.RootElement;

    return IsSuccessHttpResponse(response)?
      MapOAuthOptionsClaims(authOptions, userData):
      GetOAuthErrorType(userData);
  }
}