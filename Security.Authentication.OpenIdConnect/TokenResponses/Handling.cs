using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async Task<TokenResult> HandleTokenResponse(
    HttpResponseMessage response,
    string authCode,
    CancellationToken cancellationToken = default)
  {
    // var contentMediaType = responseMessage.Content.Headers.ContentType?.MediaType;
    // if (Logger.IsEnabled(LogLevel.Debug))
    // {
    //     if (string.IsNullOrEmpty(contentMediaType))
    //     {
    //         Logger.LogDebug($"Unexpected token response format. Status Code: {(int)responseMessage.StatusCode}. Content-Type header is missing.");
    //     }
    //     else if (!string.Equals(contentMediaType, "application/json", StringComparison.OrdinalIgnoreCase))
    //     {
    //         Logger.LogDebug($"Unexpected token response format. Status Code: {(int)responseMessage.StatusCode}. Content-Type {responseMessage.Content.Headers.ContentType}.");
    //     }
    // }

    // // Error handling:
    // // 1. If the response body can't be parsed as json, throws.
    // // 2. If the response's status code is not in 2XX range, throw OpenIdConnectProtocolException. If the body is correct parsed,
    // //    pass the error information from body to the exception.
    // OpenIdConnectMessage message;
    // try
    // {
    //     var responseContent = await responseMessage.Content.ReadAsStringAsync(Context.RequestAborted);
    //     message = new OpenIdConnectMessage(responseContent);
    // }
    // catch (Exception ex)
    // {
    //     throw new OpenIdConnectProtocolException($"Failed to parse token response body as JSON. Status Code: {(int)responseMessage.StatusCode}. Content-Type: {responseMessage.Content.Headers.ContentType}", ex);
    // }

    // if (!responseMessage.IsSuccessStatusCode)
    // {
    //     throw CreateOpenIdConnectProtocolException(message, responseMessage);
    // }

    var responseContent = await ReadTokenResponseContent(response, cancellationToken);
    return default!;
  }
}