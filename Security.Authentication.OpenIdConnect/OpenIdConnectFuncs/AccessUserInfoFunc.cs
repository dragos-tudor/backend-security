
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OpenIdConnect;

public delegate Task<UserInfoResult> AccessUserInfoFunc<TOptions> (
  TOptions authOptions,
  string accessToken,
  HttpClient httpClient,
  CancellationToken cancellationToken = default
) where TOptions: OpenIdConnectOptions;