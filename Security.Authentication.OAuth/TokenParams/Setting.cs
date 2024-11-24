
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal const string GrantAuthorizationCode = "authorization_code";

  public static OAuthParams SetTokenParams(
    OAuthParams authParams,
    AuthenticationProperties authProps,
    OAuthOptions authOptions,
    string authCode)
  {
    SetOAuthParam(authParams, OAuthParamNames.ClientId, authOptions.ClientId);
    SetOAuthParam(authParams, OAuthParamNames.ClientSecret, authOptions.ClientSecret);
    SetOAuthParam(authParams, OAuthParamNames.GrantType, GrantAuthorizationCode);
    SetOAuthParam(authParams, OAuthParamNames.AuthorizationCode, authCode);
    SetOAuthParam(authParams, OAuthParamNames.RedirectUri, GetAuthPropsCallbackUri(authProps)!);
    if(ShouldUseCodeChallenge(authOptions)) SetOAuthParam(authParams, OAuthParamNames.CodeVerifier, GetAuthPropsCodeVerifier(authProps)!);
    return authParams;
  }
}