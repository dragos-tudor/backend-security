
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

public delegate Task<TokenResult> ExchangeCodeForTokensFunc<TOptions> (
  TOptions authOptions,
  AuthenticationProperties authProperties,
  string authCode,
  HttpClient httpClient,
  string? codeVerifier = default,
  CancellationToken cancellationToken = default
) where TOptions: OAuthOptions;