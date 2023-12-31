
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Security.Authentication.OAuth;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs {

  const string BasicSchema = "Basic";

  public static async Task<TokenResult> ExchangeTwitterCodeForTokensAsync (
    TwitterOptions twitterOptions,
    AuthenticationProperties authProperties,
    string authCode,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  {
    var request = BuildTokenRequest(twitterOptions, authProperties, authCode, httpClient);
    SetAuthorizationHeader(request, BasicSchema, GetTwitterCredentials(twitterOptions.ClientId, twitterOptions.ClientSecret));
    using var response = await SendTokenRequestAsync(request, httpClient, cancellationToken);
    return await HandleTokenResponseAsync(response, cancellationToken);
  }

  public static Task<TokenResult> ExchangeTwitterCodeForTokensAsync (
    HttpContext context,
    AuthenticationProperties authProperties,
    string authCode,
    CancellationToken cancellationToken = default) =>
      ExchangeCodeForTokensAsync(
        ResolveService<TwitterOptions>(context),
        authProperties,
        authCode,
        ResolveService<HttpClient>(context),
        cancellationToken
      );

}