
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<UserInfoResult> AccessUserInfo<TOptions>(
    string accessToken,
    TOptions authOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default) where TOptions: OAuthOptions
  {
    using var request = BuildUserInfoRequest(authOptions.UserInfoEndpoint, accessToken);
    using var response = await SendHttpRequest(request, httpClient, cancellationToken);
    return await HandleUserInfoResponse(response, authOptions, cancellationToken);
  }
}