
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<TokenResult> ExchangeCodeForTokens<TOptions> (
    string authCode,
    AuthenticationProperties authProperties,
    TOptions authOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions
  {
    var tokenParams = BuildTokenParams(authProperties, authOptions, authCode);
    var request = BuildTokenRequest(authOptions.TokenEndpoint, tokenParams, httpClient.DefaultRequestVersion);
    using var response = await SendTokenRequest(request, httpClient, cancellationToken);
    return await HandleTokenResponse(response, cancellationToken);
  }

  public static Task<TokenResult> ExchangeCodeForTokens<TOptions> (
    HttpContext context,
    string authCode,
    AuthenticationProperties authProperties,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions =>
    ExchangeCodeForTokens(
      authCode,
      authProperties,
      ResolveService<TOptions>(context),
      ResolveHttpClient<TOptions>(context),
      cancellationToken
    );

}