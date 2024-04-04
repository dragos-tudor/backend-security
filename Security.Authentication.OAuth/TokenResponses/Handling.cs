
using System.Net.Http;
using System.Threading;
using static System.Text.Json.JsonDocument;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<TokenResult> HandleTokenResponse (
    HttpResponseMessage response,
    CancellationToken cancellationToken = default)
  {
    var responseContent = await ReadTokenResponseContent(response, cancellationToken);
    using var tokensData = Parse(responseContent);

    return response.IsSuccessStatusCode ?
      ValidateTokenResult(tokensData.RootElement):
      IsJsonTokenError(tokensData) ?
        BuildTokenErrorFromJson(tokensData.RootElement) :
        BuildTokenErrorFromResponse(response, responseContent);
  }

}