
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OAuth;

public delegate Task<UserInfoResult> AccessUserInfoFunc<TOptions> (
  TOptions authOptions,
  string accessToken,
  HttpClient httpClient,
  CancellationToken cancellationToken = default
) where TOptions: OAuthOptions;