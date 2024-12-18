
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

public delegate Task<TokenResult> ExchangeCodeForTokensFunc<TOptions>(
    string authCode,
    AuthenticationProperties authProps,
    TOptions oauthOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions : OAuthOptions;