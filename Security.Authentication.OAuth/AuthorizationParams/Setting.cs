
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static OAuthParams SetAuthorizationOAuthParams(
    OAuthParams oauthParams,
    OAuthOptions oauthOptions,
    string state,
    string callbackUrl)
  {
    // https://www.ietf.org/rfc/rfc6749.txt [Authorization Request page 25]
    SetOAuthParam(oauthParams, OAuthParamNames.ClientId, oauthOptions.ClientId);
    SetOAuthParam(oauthParams, OAuthParamNames.ResponseType, oauthOptions.ResponseType);
    SetOAuthParam(oauthParams, OAuthParamNames.RedirectUri, callbackUrl);
    SetOAuthParam(oauthParams, OAuthParamNames.Scope, FormatOAuthScopes(oauthOptions));
    SetOAuthParam(oauthParams, OAuthParamNames.State, state);
    return oauthParams;
  }
}