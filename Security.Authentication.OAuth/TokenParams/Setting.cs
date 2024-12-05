
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal const string GrantAuthorizationCode = "authorization_code";

  public static OAuthParams SetTokenParams(
    OAuthParams oauthParams,
    AuthenticationProperties authProps,
    OAuthOptions oauthOptions,
    string authCode)
  {
    SetOAuthParam(oauthParams, OAuthParamNames.ClientId, oauthOptions.ClientId);
    SetOAuthParam(oauthParams, OAuthParamNames.ClientSecret, oauthOptions.ClientSecret);
    SetOAuthParam(oauthParams, OAuthParamNames.GrantType, GrantAuthorizationCode);
    SetOAuthParam(oauthParams, OAuthParamNames.AuthorizationCode, authCode);
    SetOAuthParam(oauthParams, OAuthParamNames.RedirectUri, GetAuthPropsCallbackUri(authProps)!);
    if (ShouldUseCodeChallenge(oauthOptions)) SetOAuthParam(oauthParams, OAuthParamNames.CodeVerifier, GetAuthPropsCodeVerifier(authProps)!);
    return oauthParams;
  }
}