
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

public delegate Task<TokenResult> ExchangeCodeForTokensFunc<TOptions> (
  string authCode,
  AuthenticationProperties authProperties,
  TOptions oidcOptions,
  OpenIdConnectConfiguration oidcConfigurations,
  StringDataFormat stringDataFormat,
  HttpClient httpClient,
  IRequestCookieCollection cookies,
  CancellationToken cancellationToken = default
) where TOptions: OpenIdConnectOptions;