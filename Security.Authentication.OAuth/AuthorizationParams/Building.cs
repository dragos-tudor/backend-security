using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static IDictionary<string, string> BuildAuthorizationParams<TOptions>(
    AuthenticationProperties authProperties,
    TOptions authOptions,
    string redirectUri,
    string state,
    string responseType = "code") where TOptions : OAuthOptions
  {
    // https://www.ietf.org/rfc/rfc6749.txt [Authorization Request page 25]
    var authParams = new Dictionary<string, string>();
    SetAuthorizationParamClientId(authParams, authOptions.ClientId);
    SetAuthorizationParamResponseType(authParams, responseType);
    SetAuthorizationParamRedirectUri(authParams, redirectUri);
    SetAuthorizationParamScope(authParams, FormatOAuthScopes(authOptions));
    SetAuthorizationParamState(authParams, state);

    if (ShouldUseCodeChallenge(authOptions))
      SetAuthorizationParamsCodeChallenge(authProperties, authParams);

    return authParams;
  }


}