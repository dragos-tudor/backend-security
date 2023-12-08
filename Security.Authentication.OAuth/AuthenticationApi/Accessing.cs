
using System.Threading;

namespace Security.Authentication.OAuth;

partial class Funcs {

  public static async Task<UserInfoResult> AccessUserInfoAsync<TOptions> (
    TOptions authOptions,
    string accessToken,
    CancellationToken cancellationToken) where TOptions: OAuthOptions
  {
    var request = BuildUserInfoRequest(authOptions.UserInformationEndpoint, accessToken);
    using var response = await SendUserInfoRequestAsync(request, authOptions.RemoteClient, cancellationToken);
    return await HandleUserInfoResponseAsync(response, authOptions, cancellationToken);
  }

}