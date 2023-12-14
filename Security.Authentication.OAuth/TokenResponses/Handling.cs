
using System.Net.Http;
using System.Threading;
using static System.Text.Json.JsonDocument;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<TokenResult> HandleTokenResponseAsync (
    HttpResponseMessage response,
    CancellationToken cancellationToken = default)
  {
    var responseContent = await ReadTokenResponseContentAsync(response, cancellationToken);
    using var responseJson = Parse(responseContent);

    return response.IsSuccessStatusCode ?
      ValidateTokenResult(CreateSuccessTokenResult(responseJson.RootElement)):
      IsJsonTokenError(responseJson) ?
        CreateFailureTokenResult(BuildTokenErrorFromJson(responseJson.RootElement)) :
        CreateFailureTokenResult(BuildTokenErrorFromResponse(response, responseContent));
  }

}