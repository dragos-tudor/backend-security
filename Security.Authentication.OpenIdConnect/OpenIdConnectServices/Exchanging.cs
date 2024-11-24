
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs {

  public static async Task<TokenResult> ExchangeCodeForTokens<TOptions>(
    string authCode,
    AuthenticationProperties authProps,
    TOptions oidcOptions,
    StringDataFormat stringDataFormat,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions: OpenIdConnectOptions
  {
    var tokenParams = BuildTokenParams(authProps, oidcOptions, authCode);
    using var request = BuildTokenRequest(oidcConfiguration.TokenEndpoint, tokenParams, httpClient.DefaultRequestVersion);
    using var response = await SendHttpRequest(request, httpClient, cancellationToken);
    return await HandleTokenResponse(response, authProps, oidcOptions, stringDataFormat, cancellationToken);
  }
}