
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs {

  public static async Task<TokenResult> ExchangeCodeForTokens<TOptions> (
    AuthenticationProperties authProperties,
    string authCode,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions: OpenIdConnectOptions
  {
    var tokenParams = BuildTokenParams(authProperties, oidcOptions, authCode);
    var request = BuildTokenRequest(oidcConfiguration.TokenEndpoint, tokenParams, httpClient.DefaultRequestVersion);
    using var response = await SendTokenRequest(request, httpClient, cancellationToken);
    return await HandleTokenResponse(response, authCode, cancellationToken);
  }

  public static Task<TokenResult> ExchangeCodeForTokens<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties,
    string authCode,
    CancellationToken cancellationToken = default)
  where TOptions: OpenIdConnectOptions =>
      ExchangeCodeForTokens(
        authProperties,
        authCode,
        ResolveService<TOptions>(context),
        ResolveService<OpenIdConnectConfiguration>(context),
        ResolveService<HttpClient>(context),
        cancellationToken
      );

}