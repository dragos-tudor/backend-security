
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Security.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs {

  public static async Task<UserInfoResult> AccessTwitterUserInfo (
    TwitterOptions twitterOptions,
    string accessToken,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  {
    var requestUri = QueryHelpers.AddQueryString(twitterOptions.UserInformationEndpoint, BuildSpecificUserInfoParams(twitterOptions));
    var request = BuildUserInfoRequest(requestUri, accessToken);
    using var response = await SendUserInfoRequest(request, httpClient, cancellationToken);
    return await HandleUserInfoResponse(response, twitterOptions, cancellationToken);
  }

  public static Task<UserInfoResult> AccessTwitterUserInfo (
    HttpContext context,
    string accessToken,
    CancellationToken cancellationToken = default) =>
      AccessTwitterUserInfo(
        ResolveService<TwitterOptions>(context),
        accessToken,
        ResolveService<HttpClient>(context),
        cancellationToken
      );

}