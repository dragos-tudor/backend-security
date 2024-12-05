
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<TokenResult> ExchangeCodeForTokens<TOptions>(
    string code,
    AuthenticationProperties authProps,
    TOptions oauthOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions
  {
    var tokenParams = BuildTokenParams(authProps, oauthOptions, code);
    using var request = BuildTokenRequest(oauthOptions, tokenParams, httpClient.DefaultRequestVersion);

    using var response = await SendHttpRequest(request, httpClient, cancellationToken);
    return await HandleTokenResponse(response, cancellationToken);
  }
}