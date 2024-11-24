
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<TokenResult> ExchangeCodeForTokens<TOptions>(
    string authCode,
    AuthenticationProperties authProps,
    TOptions authOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions
  {
    var tokenParams = BuildTokenParams(authProps, authOptions, authCode);
    using var request = BuildTokenRequest(authOptions, tokenParams, httpClient.DefaultRequestVersion);

    using var response = await SendHttpRequest(request, httpClient, cancellationToken);
    return await HandleTokenResponse(response, cancellationToken);
  }
}