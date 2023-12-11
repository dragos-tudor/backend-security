
using System.Threading;
using System.Threading.Tasks;
using Security.Authentication.OAuth;
using static Microsoft.AspNetCore.WebUtilities.QueryHelpers;

namespace Security.Authentication.Twitter;

partial class Funcs {

  public static async Task<UserInfoResult> AccessTwitterUserInfoAsync (
    TwitterOptions twitterOptions,
    string accessToken,
    CancellationToken cancellationToken = default)
  {
    var requestUri = AddQueryString(twitterOptions.UserInformationEndpoint, BuildSpecificUserInfoParams(twitterOptions));
    var request = BuildUserInfoRequest(requestUri, accessToken);
    using var response = await SendUserInfoRequestAsync(request, twitterOptions.RemoteClient, cancellationToken);
    return await HandleUserInfoResponseAsync(response, twitterOptions, cancellationToken);
  }

}