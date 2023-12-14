
using System.Net.Http;
using System.Net.Mime;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static HttpRequestMessage BuildTokenRequest<TOptions>(
    TOptions authOptions,
    AuthenticationProperties authProperties,
    string authCode,
    HttpClient httpClient)
  where TOptions : OAuthOptions
  {
    var tokenRequest = CreateTokenRequest(authOptions.TokenEndpoint);
    var tokenParams = CreateTokenParams(authOptions, authCode, GetAuthenticationPropertiesCallbackUri(authProperties)!);

    if (authOptions.UsePkce) {
      AddTokenCodeVerifierParam(tokenParams, GetAuthenticationPropertiesCodeVerifier(authProperties)!);
      RemoveAuthenticationPropertiesCodeVerifier(authProperties);
    }
    SetTokenRequestAcceptType(tokenRequest, MediaTypeNames.Application.Json);
    SetTokenRequestVersion(tokenRequest, httpClient.DefaultRequestVersion);
    SetTokenRequestContent(tokenRequest, tokenParams);

    return tokenRequest;
  }

}