
namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal const string GrantAuthorizationCode = "authorization_code";

  public static OAuthParams SetTokenParams(
    OAuthParams oauthParams,
    OAuthOptions oauthOptions,
    string authCode,
    string callbackUrl,
    string? codeVerifier)
  {
    SetOAuthParam(oauthParams, OAuthParamNames.ClientId, oauthOptions.ClientId);
    SetOAuthParam(oauthParams, OAuthParamNames.ClientSecret, oauthOptions.ClientSecret);
    SetOAuthParam(oauthParams, OAuthParamNames.GrantType, GrantAuthorizationCode);
    SetOAuthParam(oauthParams, OAuthParamNames.AuthorizationCode, authCode);
    SetOAuthParam(oauthParams, OAuthParamNames.RedirectUri, callbackUrl);
    if (IsNotEmptyString(codeVerifier)) SetOAuthParam(oauthParams, OAuthParamNames.CodeVerifier, codeVerifier!);
    return oauthParams;
  }
}