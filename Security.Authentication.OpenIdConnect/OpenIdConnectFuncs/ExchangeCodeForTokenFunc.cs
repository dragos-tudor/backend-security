
using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

public delegate Task<TokenResult> ExchangeCodeForTokensFunc<TOptions>(
    string code,
    AuthenticationProperties authProps,
    TOptions oidcOptions,
    OpenIdConnectValidationOptions validationOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions : OpenIdConnectOptions;