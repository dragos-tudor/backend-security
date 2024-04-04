
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs {

  public static async Task<TokenResult> ExchangeCodeForTokens<TOptions> (
    string authCode,
    AuthenticationProperties authProperties,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    StringDataFormat stringDataFormat,
    HttpClient httpClient,
    IRequestCookieCollection cookies,
    CancellationToken cancellationToken = default)
  where TOptions: OpenIdConnectOptions
  {
    var tokenParams = BuildTokenParams(authProperties, oidcOptions, authCode);
    var request = BuildTokenRequest(oidcConfiguration.TokenEndpoint, tokenParams, httpClient.DefaultRequestVersion);
    using var response = await SendTokenRequest(request, httpClient, cancellationToken);
    return await HandleTokenResponse(response, authProperties, oidcOptions, oidcConfiguration, stringDataFormat, cookies, cancellationToken);
  }

  public static Task<TokenResult> ExchangeCodeForTokens<TOptions> (
    HttpContext context,
    string authCode,
    AuthenticationProperties authProperties,
    CancellationToken cancellationToken = default)
  where TOptions: OpenIdConnectOptions =>
      ExchangeCodeForTokens(
        authCode,
        authProperties,
        ResolveService<TOptions>(context),
        ResolveService<OpenIdConnectConfiguration>(context),
        ResolveStringDataFormat<TOptions>(context),
        ResolveHttpClient<TOptions>(context),
        GetRequestCookies(context.Request),
        cancellationToken
      );

}