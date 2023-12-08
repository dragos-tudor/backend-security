
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class Funcs {

  public static async Task<TokenResult> ExchangeCodeForTokensAsync<TOptions> (
    TOptions authOptions,
    AuthenticationProperties authProperties,
    string authCode,
    CancellationToken cancellationToken) where TOptions: OAuthOptions
  {
    var request = BuildTokenRequest(authOptions, authProperties, authCode);
    using var response = await SendTokenRequestAsync(request, authOptions.RemoteClient, cancellationToken);
    return await HandleTokenResponseAsync(response, cancellationToken);
  }

}