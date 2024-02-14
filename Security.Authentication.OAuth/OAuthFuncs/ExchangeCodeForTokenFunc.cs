
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

public delegate Task<TokenResult> ExchangeCodeForTokensFunc<TOptions> (
  string authCode,
  AuthenticationProperties authProperties,
  TOptions authOptions,
  HttpClient httpClient,
  CancellationToken cancellationToken = default
) where TOptions: OAuthOptions;