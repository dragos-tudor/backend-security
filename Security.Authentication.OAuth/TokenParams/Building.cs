using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static IDictionary<string, string> BuildTokenParams(
    AuthenticationProperties authProperties,
    OAuthOptions authOptions,
    string authCode)
  {
    var tokenParams = new Dictionary<string, string>();

    SetTokenParamClientId(tokenParams, authOptions.ClientId);
    SetTokenParamClientSecret(tokenParams, authOptions.ClientSecret);
    SetTokenParamGrantType(tokenParams, GrantAuthorizationCode);
    SetTokenParamAuthorizationCode(tokenParams, authCode);
    SetTokenParamRedirectUri(tokenParams, GetAuthenticationPropertiesCallbackUri(authProperties)!);
    if(GetAuthenticationPropertiesCodeVerifier(authProperties!) is string codeVerifier)
      SetTokenParamCodeVerifier(tokenParams, codeVerifier!);

    return tokenParams;
  }
}