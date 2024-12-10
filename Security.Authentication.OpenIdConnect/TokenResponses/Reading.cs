
using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async Task<OidcTokens> ReadTokenOidcTokens(HttpResponseMessage response, CancellationToken cancellationToken)
  {
    var tokenResponse = await ReadHttpResponseContent(response, cancellationToken);
    var tokenData = ToOpenIdConnectData(tokenResponse);
    return CreateOidcTokens(tokenData);
  }
}