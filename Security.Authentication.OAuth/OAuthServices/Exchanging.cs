
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
    string? codeVerifier = default,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions
  {
    var tokenParams = BuildTokenParams(authProperties, authOptions, authCode, codeVerifier);
    var request = BuildTokenRequest(authOptions.TokenEndpoint, tokenParams, httpClient.DefaultRequestVersion);
    using var response = await SendTokenRequest(request, httpClient, cancellationToken);
    return await HandleTokenResponse(response, cancellationToken);
  }

  public static Task<TokenResult> ExchangeCodeForTokens<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties,
    string authCode,
    string? codeVerifier,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions =>
    ExchangeCodeForTokens(
      ResolveService<TOptions>(context),
      authProperties,
      authCode,
      ResolveService<HttpClient>(context),
      codeVerifier,
      cancellationToken
    );

}