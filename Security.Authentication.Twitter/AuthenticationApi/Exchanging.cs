
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Security.Authentication.OAuth;

namespace Security.Authentication.Twitter;

partial class Funcs {

  const string BasicSchema = "Basic";

  internal static async Task<TokenResult> ExchangeTwitterCodeForTokensAsync (
    TwitterOptions twitterOptions,
    AuthenticationProperties authProperties,
    string authCode,
    CancellationToken cancellationToken)
  {
    var request = BuildTokenRequest(twitterOptions, authProperties, authCode);
    SetAuthorizationHeader(request, BasicSchema, GetTwitterCredentials(twitterOptions.ClientId, twitterOptions.ClientSecret));
    using var response = await SendTokenRequestAsync(request, twitterOptions.RemoteClient, cancellationToken);
    return await HandleTokenResponseAsync(response, cancellationToken);
  }

}