
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Security.Authentication.OAuth;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs {

  public static async Task<UserInfoResult> AccessFacebookUserInfo (
    FacebookOptions facebookOptions,
    string accessToken,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  {
    var userInfoParams = BuildSpecificUserInfoParams(facebookOptions, accessToken);
    var requestUri = QueryHelpers.AddQueryString(facebookOptions.UserInformationEndpoint, userInfoParams);
    var request = BuildUserInfoRequest(requestUri, accessToken);

    using var response = await SendUserInfoRequest(request, httpClient, cancellationToken);
    return await HandleUserInfoResponse(response, facebookOptions, cancellationToken);
  }

  public static Task<UserInfoResult> AccessFacebookUserInfo (
    HttpContext context,
    FacebookOptions facebookOptions,
    string accessToken,
    CancellationToken cancellationToken = default) =>
      AccessFacebookUserInfo(
        facebookOptions,
        accessToken,
        ResolveService<HttpClient>(context),
        cancellationToken
      );

}