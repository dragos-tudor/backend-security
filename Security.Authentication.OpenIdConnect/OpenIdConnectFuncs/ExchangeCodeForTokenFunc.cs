
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

public delegate Task<TokenResult> ExchangeCodeForTokensFunc<TOptions> (
  TOptions authOptions,
  AuthenticationProperties authProperties,
  string authCode,
  HttpClient httpClient,
  CancellationToken cancellationToken = default
) where TOptions: OpenIdConnectOptions;