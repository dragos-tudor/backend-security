
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<TokenResult> ExchangeCodeForTokensAsync<TOptions> (
    TOptions authOptions,
    AuthenticationProperties authProperties,
    string authCode,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions
  {
    var request = BuildTokenRequest(authOptions, authProperties, authCode, httpClient);
    using var response = await SendTokenRequestAsync(request, httpClient, cancellationToken);
    return await HandleTokenResponseAsync(response, cancellationToken);
  }

  public static Task<TokenResult> ExchangeCodeForTokensAsync<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties,
    string authCode,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions =>
    ExchangeCodeForTokensAsync(
      ResolveService<TOptions>(context),
      authProperties,
      authCode,
      ResolveService<HttpClient>(context),
      cancellationToken
    );

}