
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<UserInfoResult> HandleUserInfoResponse (
    HttpResponseMessage response,
    OAuthOptions authOptions,
    CancellationToken cancellationToken = default)
  {
    var responseContent = await ReadUserInfoResponseContent(response, cancellationToken);

    return response.IsSuccessStatusCode ?
      CreateSuccessUserInfoResult(AddClaimsPrincipalClaims(CreatePrincipal(authOptions.SchemeName), authOptions, responseContent)) :
      CreateFailureUserInfoResult(BuildUserInfoError(response, responseContent));
  }

}