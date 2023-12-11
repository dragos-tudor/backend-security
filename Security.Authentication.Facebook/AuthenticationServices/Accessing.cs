
using System.Threading;
using System.Threading.Tasks;
using Security.Authentication.OAuth;
using static Microsoft.AspNetCore.WebUtilities.QueryHelpers;

namespace Security.Authentication.Facebook;

partial class Funcs {

  public static async Task<UserInfoResult> AccessFacebookUserInfoAsync (
    FacebookOptions facebookOptions,
    string accessToken,
    CancellationToken cancellationToken = default)
  {
    var requestUri = AddQueryString(facebookOptions.UserInformationEndpoint, BuildSpecificUserInfoParams(facebookOptions, accessToken));
    var request = BuildUserInfoRequest(requestUri, accessToken);
    using var response = await SendUserInfoRequestAsync(request, facebookOptions.RemoteClient, cancellationToken);
    return await HandleUserInfoResponseAsync(response, facebookOptions, cancellationToken);
  }

}