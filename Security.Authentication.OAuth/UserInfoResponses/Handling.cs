
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
    return IsSuccessUserInfoResponse(response)?
      BuildClaimsPrincipal(authOptions, responseContent):
      BuildUserInfoError(response, responseContent);
  }
}