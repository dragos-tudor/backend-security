
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<UserInfoResult> AccessUserInfoAsync<TOptions> (
    TOptions authOptions,
    string accessToken,
    HttpClient httpClient,
    CancellationToken cancellationToken = default) where TOptions: OAuthOptions
  {
    var request = BuildUserInfoRequest(authOptions.UserInformationEndpoint, accessToken);
    using var response = await SendUserInfoRequestAsync(request, httpClient, cancellationToken);
    return await HandleUserInfoResponseAsync(response, authOptions, cancellationToken);
  }

  public static Task<UserInfoResult> AccessUserInfoAsync<TOptions> (
    HttpContext context,
    TOptions authOptions,
    string accessToken,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions =>
    AccessUserInfoAsync(
      authOptions,
      accessToken,
      ResolveService<HttpClient>(context),
      cancellationToken
    );

}