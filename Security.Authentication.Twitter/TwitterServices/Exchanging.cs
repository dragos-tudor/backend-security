
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

  public static async Task<TokenResult> ExchangeTwitterCodeForTokens (
    TwitterOptions twitterOptions,
    AuthenticationProperties authProperties,
    string authCode,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  {
    var tokenParams = BuildTokenParams(authProperties, twitterOptions, authCode);
    var request = BuildTokenRequest(twitterOptions.TokenEndpoint, tokenParams, httpClient.DefaultRequestVersion);
    SetAuthorizationHeader(request, BasicSchema, GetTwitterCredentials(twitterOptions.ClientId, twitterOptions.ClientSecret));
    using var response = await SendTokenRequest(request, httpClient, cancellationToken);
    return await HandleTokenResponse(response, cancellationToken);
  }

  public static Task<TokenResult> ExchangeTwitterCodeForTokens (
    HttpContext context,
    AuthenticationProperties authProperties,
    string authCode,
    CancellationToken cancellationToken = default) =>
      ExchangeCodeForTokens(
        ResolveService<TwitterOptions>(context),
        authProperties,
        authCode,
        ResolveService<HttpClient>(context),
        cancellationToken
      );

}