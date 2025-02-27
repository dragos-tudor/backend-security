
using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<UserInfoResult> AccessUserInfo<TOptions>(
    string accessToken,
    TOptions oauthOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions : OAuthOptions
  {
    using var request = BuildUserInfoRequest(oauthOptions.UserInfoEndpoint, accessToken);
    using var response = await SendHttpRequest(request, httpClient, cancellationToken);
    return await HandleUserInfoResponse(response, oauthOptions, cancellationToken);
  }
}