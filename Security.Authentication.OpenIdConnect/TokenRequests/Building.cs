using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static HttpRequestMessage BuildTokenRequest(
    string tokenEndpoint,
    IDictionary<string, string> tokenParams,
    Version requestVersion)
  {
    var tokenRequest = CreateTokenRequest(tokenEndpoint);

    SetTokenRequestAcceptType(tokenRequest, MediaTypeNames.Application.Json);
    SetTokenRequestContent(tokenRequest, tokenParams);
    SetTokenRequestVersion(tokenRequest, requestVersion);

    return tokenRequest;
  }
}