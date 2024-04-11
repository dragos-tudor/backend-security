
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<UserInfoResult> AccessUserInfo<TOptions> (
    string accessToken,
    TOptions authOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default) where TOptions: OAuthOptions
  {
    using var request = BuildUserInfoRequest(authOptions.UserInformationEndpoint, accessToken);
    using var response = await SendUserInfoRequest(request, httpClient, cancellationToken);
    return await HandleUserInfoResponse(response, authOptions, cancellationToken);
  }

  public static Task<UserInfoResult> AccessUserInfo<TOptions> (
    HttpContext context,
    string accessToken,
    TOptions authOptions,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions =>
    AccessUserInfo(
      accessToken,
      authOptions,
      ResolveHttpClient<TOptions>(context),
      cancellationToken
    );

}