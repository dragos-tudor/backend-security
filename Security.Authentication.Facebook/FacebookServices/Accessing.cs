
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Security.Authentication.OAuth;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static async Task<UserInfoResult> AccessFacebookUserInfo(
    string accessToken,
    FacebookOptions facebookOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  {
    var requestParams = BuildFacebookUserInfoParams(facebookOptions, accessToken);
    var requestUri = BuildHttpRequestUri(facebookOptions.UserInfoEndpoint, requestParams);
    var request = BuildUserInfoRequest(requestUri, accessToken);

    using var response = await SendHttpRequest(request, httpClient, cancellationToken);
    return await HandleUserInfoResponse(response, facebookOptions, cancellationToken);
  }
}