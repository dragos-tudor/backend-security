
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static HttpRequestMessage BuildTokenRequest(
    string tokenEndpoint,
    IDictionary<string, string> tokenParams,
    Version requestVersion)
  {
    var tokenRequest = CreateTokenRequest(tokenEndpoint);

    SetTokenRequestAcceptType(tokenRequest, MediaTypeNames.Application.Json);
    SetTokenRequestVersion(tokenRequest, requestVersion);
    SetTokenRequestContent(tokenRequest, tokenParams);

    return tokenRequest;
  }

}