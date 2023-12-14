
using System.Net.Http;
using System.Threading;
using static System.Text.Json.JsonDocument;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<UserInfoResult> HandleUserInfoResponseAsync (
    HttpResponseMessage response,
    OAuthOptions authOptions,
    CancellationToken cancellationToken = default)
  {
    var responseContent = await ReadUserInfoResponseContentAsync(response, cancellationToken);
    using var responseJson = Parse(responseContent);
    return response.IsSuccessStatusCode ?
      CreateSuccessUserInfoResult(BuildClaimsPrincipalWithClaims(responseJson, authOptions.ClaimActions, GetClaimsIssuer(authOptions), authOptions.SchemeName)) :
      CreateFailureUserInfoResult(BuildUserInfoErrorFromResponse(response, responseContent));
  }

}