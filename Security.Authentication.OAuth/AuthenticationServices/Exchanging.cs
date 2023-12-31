
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<TokenResult> ExchangeCodeForTokens<TOptions> (
    TOptions authOptions,
    AuthenticationProperties authProperties,
    string authCode,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions
  {
    var request = BuildTokenRequest(authOptions, authProperties, authCode, httpClient);
    using var response = await SendTokenRequest(request, httpClient, cancellationToken);
    return await HandleTokenResponse(response, cancellationToken);
  }

  public static Task<TokenResult> ExchangeCodeForTokens<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties,
    string authCode,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions =>
    ExchangeCodeForTokens(
      ResolveService<TOptions>(context),
      authProperties,
      authCode,
      ResolveService<HttpClient>(context),
      cancellationToken
    );

}