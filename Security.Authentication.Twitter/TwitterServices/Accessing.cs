
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Security.Authentication.OAuth;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static async Task<UserInfoResult> AccessTwitterUserInfo(
    string accessToken,
    TwitterOptions twitterOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  {
    var requestParams = BuildSpecificUserInfoParams(twitterOptions);
    var requestUri = BuildHttpRequestUri(twitterOptions.UserInfoEndpoint, requestParams);
    var request = BuildUserInfoRequest(requestUri, accessToken);

    using var response = await SendHttpRequest(request, httpClient, cancellationToken);
    return await HandleUserInfoResponse(response, twitterOptions, cancellationToken);
  }
}