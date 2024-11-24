
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Security.Authentication.OAuth;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  const string BasicSchema = "Basic";

  public static async Task<TokenResult> ExchangeTwitterCodeForTokens(
    string authCode,
    AuthenticationProperties authProps,
    TwitterOptions twitterOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  {
    var tokenParams = BuildTokenParams(authProps, twitterOptions, authCode);
    using var request = BuildTokenRequest(twitterOptions, tokenParams, httpClient.DefaultRequestVersion);
    SetAuthorizationHeader(request, BasicSchema, GetTwitterCredentials(twitterOptions.ClientId, twitterOptions.ClientSecret));

    using var response = await SendHttpRequest(request, httpClient, cancellationToken);
    return await HandleTokenResponse(response, cancellationToken);
  }
}