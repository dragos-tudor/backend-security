
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Security.Authentication.OAuth;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs {

  public static async Task<UserInfoResult> AccessFacebookUserInfoAsync (
    FacebookOptions facebookOptions,
    string accessToken,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  {
    var userInfoParams = BuildSpecificUserInfoParams(facebookOptions, accessToken);
    var requestUri = QueryHelpers.AddQueryString(facebookOptions.UserInformationEndpoint, userInfoParams);
    var request = BuildUserInfoRequest(requestUri, accessToken);

    using var response = await SendUserInfoRequestAsync(request, httpClient, cancellationToken);
    return await HandleUserInfoResponseAsync(response, facebookOptions, cancellationToken);
  }

  public static Task<UserInfoResult> AccessFacebookUserInfoAsync (
    HttpContext context,
    FacebookOptions facebookOptions,
    string accessToken,
    CancellationToken cancellationToken = default) =>
      AccessFacebookUserInfoAsync(
        facebookOptions,
        accessToken,
        ResolveService<HttpClient>(context),
        cancellationToken
      );

}