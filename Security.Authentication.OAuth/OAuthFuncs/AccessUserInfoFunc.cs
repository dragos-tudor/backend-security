
using System.Net.Http;

namespace Security.Authentication.OAuth;

public delegate Task<UserInfoResult> AccessUserInfoFunc<TOptions>(
    string accessToken,
    TOptions oauthOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions : OAuthOptions;