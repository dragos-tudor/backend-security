
using System.Net.Http;
using System.Net.Mime;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class Funcs {

  public static HttpRequestMessage BuildTokenRequest<TOptions>(
    TOptions authOptions,
    AuthenticationProperties authProperties,
    string authCode) where TOptions : OAuthOptions
  {
    var tokenRequest = CreateTokenRequest(authOptions.TokenEndpoint);
    var tokenParams = CreateTokenParams(authOptions, authCode, GetAuthenticationPropertiesCallbackUri(authProperties)!);

    if (authOptions.UsePkce) {
      AddTokenCodeVerifierParam(tokenParams, GetAuthenticationPropertiesCodeVerifier(authProperties)!);
      RemoveAuthenticationPropertiesCodeVerifier(authProperties);
    }
    SetTokenRequestAcceptType(tokenRequest, MediaTypeNames.Application.Json);
    SetTokenRequestVersion(tokenRequest, authOptions.RemoteClient.DefaultRequestVersion);
    SetTokenRequestContent(tokenRequest, tokenParams);

    return tokenRequest;
  }

}