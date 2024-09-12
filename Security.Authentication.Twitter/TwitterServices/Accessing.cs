
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Security.Authentication.OAuth;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs {

  public static async Task<UserInfoResult> AccessTwitterUserInfo (
    string accessToken,
    TwitterOptions twitterOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  {
    var requestUri = AddQueryString(twitterOptions.UserInformationEndpoint, BuildSpecificUserInfoParams(twitterOptions));
    var request = BuildUserInfoRequest(requestUri, accessToken);
    using var response = await SendUserInfoRequest(request, httpClient, cancellationToken);
    return await HandleUserInfoResponse(response, twitterOptions, cancellationToken);
  }

  public static Task<UserInfoResult> AccessTwitterUserInfo (
    HttpContext context,
    string accessToken,
    CancellationToken cancellationToken = default) =>
      AccessTwitterUserInfo(
        accessToken,
        ResolveRequiredService<TwitterOptions>(context),
        ResolveRequiredService<HttpClient>(context),
        cancellationToken
      );

}