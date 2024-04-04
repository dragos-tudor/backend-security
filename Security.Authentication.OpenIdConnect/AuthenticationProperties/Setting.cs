
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string IdToken = "IdToken";
  const string AuthorizationCode = "AuthorizationCode";

  static string? SetAuthenticationPropertiesAuthorizationCode(AuthenticationProperties authProperties, string? code) =>
    SetAuthenticationPropertiesParam(authProperties, AuthorizationCode, code);

  static string SetAuthenticationPropertiesCheckSessionIFrame(AuthenticationProperties authProperties, string checkSessionIframe) =>
      SetAuthenticationPropertiesItem(authProperties, OpenIdConnectSessionProperties.CheckSessionIFrame, checkSessionIframe!);

  static string? SetAuthenticationPropertiesIdToken(AuthenticationProperties authProperties, string? idToken) =>
    SetAuthenticationPropertiesParam(authProperties, IdToken, idToken);

  static string SetAuthenticationPropertiesRedirectUriForCode(AuthenticationProperties authProperties, string redirectUri) =>
    SetAuthenticationPropertiesItem(authProperties, OpenIdConnectDefaults.RedirectUriForCodePropertiesKey, redirectUri);

  static string SetAuthenticationPropertiesSessionState(AuthenticationProperties authProperties, string sessionState) =>
      SetAuthenticationPropertiesItem(authProperties, OpenIdConnectSessionProperties.SessionState, sessionState!);

  static string SetAuthenticationPropertiesUserState(AuthenticationProperties authProperties, string state) =>
    SetAuthenticationPropertiesItem(authProperties, OpenIdConnectDefaults.UserStatePropertiesKey, state!);

  static string CreateAuthenticationPropertiesAccessToken(AuthenticationProperties authProperties, string accessToken) =>
    SetAuthenticationPropertiesItem(authProperties, OpenIdConnectParameterNames.AccessToken, accessToken);

  static string CreateAuthenticationPropertiesIdToken(AuthenticationProperties authProperties, string idToken) =>
    SetAuthenticationPropertiesItem(authProperties, OpenIdConnectParameterNames.IdToken, idToken);

  static string CreateAuthenticationPropertiesRefreshToken(AuthenticationProperties authProperties, string refreshToken) =>
    SetAuthenticationPropertiesItem(authProperties, OpenIdConnectParameterNames.RefreshToken, refreshToken);

  static string CreateAuthenticationPropertiesTokenType(AuthenticationProperties authProperties, string tokenType) =>
    SetAuthenticationPropertiesItem(authProperties, OpenIdConnectParameterNames.TokenType, tokenType);

  static AuthenticationProperties SetAuthenticationPropertiesTokenLifetime(
    AuthenticationProperties authProperties,
    SecurityToken securityToken)
  {
    if (IsSetSecurityTokenValidFrom(securityToken))
      SetAuthenticationPropertiesIssued(authProperties, securityToken.ValidFrom);
    if (IsSetSecurityTokenValidTo(securityToken))
      SetAuthenticationPropertiesExpires(authProperties, securityToken.ValidTo);
    return authProperties;
  }

  static AuthenticationProperties SetAuthenticationPropertiesTokens(
    AuthenticationProperties authProperties,
    string? idToken,
    TokenInfo? tokenInfo) =>
      SetAuthenticationPropertiesTokens(authProperties, idToken, tokenInfo?.TokenType, tokenInfo?.AccessToken, tokenInfo?.RefreshToken);

  static AuthenticationProperties SetAuthenticationPropertiesTokens(
    AuthenticationProperties authProperties,
    string? idToken,
    string? tokenType = default,
    string? accessToken = default,
    string? refreshToken =  default)
  {
    if(IsNotEmptyString(idToken)) CreateAuthenticationPropertiesIdToken(authProperties, idToken!);
    if(IsNotEmptyString(tokenType)) CreateAuthenticationPropertiesTokenType(authProperties, tokenType!);
    if(IsNotEmptyString(accessToken)) CreateAuthenticationPropertiesAccessToken(authProperties, accessToken!);
    if(IsNotEmptyString(refreshToken)) CreateAuthenticationPropertiesRefreshToken(authProperties, refreshToken!);
    return authProperties;
  }

  static AuthenticationProperties SetAuthorizationAuthenticationProperties(
    AuthenticationProperties authProperties,
    string redirectUri,
    string redirectUriForCode,
    string state)
  {
    SetAuthenticationPropertiesRedirectUri(authProperties, redirectUri);
    SetAuthenticationPropertiesRedirectUriForCode(authProperties, redirectUriForCode);
    if (IsNotEmptyString(state))
      SetAuthenticationPropertiesUserState(authProperties, state);
    return authProperties;
  }

  static AuthenticationProperties SetPostAuthorizationAuthenticationProperties(
    AuthenticationProperties authProperties,
    OpenIdConnectMessage oidcMessage,
    OpenIdConnectOptions oidcOptions)
  {
    if(IsNotEmptyString(oidcMessage.SessionState))
      SetAuthenticationPropertiesSessionState(authProperties, oidcMessage.SessionState!);
    if (IsNotEmptyString(oidcOptions.CheckSessionIframe))
      SetAuthenticationPropertiesCheckSessionIFrame(authProperties, oidcOptions.CheckSessionIframe!);
    SetAuthenticationPropertiesIdToken(authProperties, oidcMessage.IdToken);
    SetAuthenticationPropertiesAuthorizationCode(authProperties, oidcMessage.Code);
    return authProperties;
  }

  static AuthenticationProperties SetSignoutChallengeAuthenticationProperties(
    AuthenticationProperties authProperties,
    string redirectUri,
    string alternateRedirectUri)
  {
    SetAuthenticationPropertiesRedirectUri(authProperties, redirectUri);
    if (IsEmptyUri(GetAuthenticationPropertiesRedirectUri(authProperties)))
      SetAuthenticationPropertiesRedirectUri(authProperties, alternateRedirectUri);
    return authProperties;
  }
}
