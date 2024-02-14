
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Security.Authentication.OAuth;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs {

  public static async Task<UserInfoResult> AccessFacebookUserInfo (
    string accessToken,
    FacebookOptions facebookOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  {
    var userInfoParams = BuildUserInfoParams(facebookOptions, accessToken);
    var requestUri = AddQueryString(facebookOptions.UserInformationEndpoint, userInfoParams);
    var request = BuildUserInfoRequest(requestUri, accessToken);

    using var response = await SendUserInfoRequest(request, httpClient, cancellationToken);
    return await HandleUserInfoResponse(response, facebookOptions, cancellationToken);
  }

  public static Task<UserInfoResult> AccessFacebookUserInfo (
    HttpContext context,
    string accessToken,
    FacebookOptions facebookOptions,
    CancellationToken cancellationToken = default) =>
      AccessFacebookUserInfo(
        accessToken,
        facebookOptions,
        ResolveService<HttpClient>(context),
        cancellationToken
      );

}