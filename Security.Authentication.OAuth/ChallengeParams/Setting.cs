
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static OAuthParams SetChallengeOAuthParams(
    OAuthParams oauthParams,
    OAuthOptions oauthOptions,
    string callbackUrl,
    string? codeVerifier,
    string state)
  {
    SetOAuthParam(oauthParams, OAuthParamNames.ClientId, oauthOptions.ClientId);
    SetOAuthParam(oauthParams, OAuthParamNames.ResponseType, oauthOptions.ResponseType);
    SetOAuthParam(oauthParams, OAuthParamNames.RedirectUri, callbackUrl);
    SetOAuthParam(oauthParams, OAuthParamNames.Scope, FormatOAuthScopes(oauthOptions));
    SetOAuthParam(oauthParams, OAuthParamNames.State, state);

    if (IsNotEmptyString(codeVerifier)) SetOAuthParamsCodeChallenge(oauthParams, codeVerifier!);
    SetOAuthParams(oauthParams, oauthOptions.AdditionalChallengeParameters);
    return oauthParams;
  }

  static AuthenticationProperties SetChallengeAuthProps(
    AuthenticationProperties authProps,
    string correlationId,
    string? codeVerifier,
    string? redirectUri)
  {
    SetAuthPropsCorrelationId(authProps, correlationId);
    if (IsNotEmptyString(codeVerifier)) SetAuthPropsCodeVerifier(authProps, codeVerifier!);
    if (IsNotEmptyString(redirectUri)) SetAuthPropsRedirectUri(authProps, redirectUri!);
    return authProps;
  }
}