
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<UserInfoResult> AccessUserInfo<TOptions> (
    TOptions authOptions,
    string accessToken,
    HttpClient httpClient,
    CancellationToken cancellationToken = default) where TOptions: OAuthOptions
  {
    var request = BuildUserInfoRequest(authOptions.UserInformationEndpoint, accessToken);
    using var response = await SendUserInfoRequest(request, httpClient, cancellationToken);

    return await HandleUserInfoResponse(response, authOptions, cancellationToken);
  }

  public static Task<UserInfoResult> AccessUserInfo<TOptions> (
    HttpContext context,
    TOptions authOptions,
    string accessToken,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions =>
    AccessUserInfo(
      authOptions,
      accessToken,
      ResolveService<HttpClient>(context),
      cancellationToken
    );

}