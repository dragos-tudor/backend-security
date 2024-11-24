
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

public delegate Task<TokenResult> ExchangeCodeForTokensFunc<TOptions>(string authCode, AuthenticationProperties authProps, TOptions oidcOptions, StringDataFormat stringDataFormat, HttpClient httpClient, CancellationToken cancellationToken = default) where TOptions: OpenIdConnectOptions;