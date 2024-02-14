
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

public delegate Task<TokenResult> ExchangeCodeForTokensFunc<TOptions> (
  string authCode,
  AuthenticationProperties authProperties,
  TOptions authOptions,
  OpenIdConnectConfiguration oidcConfigurations,
  HttpClient httpClient,
  CancellationToken cancellationToken = default
) where TOptions: OpenIdConnectOptions;