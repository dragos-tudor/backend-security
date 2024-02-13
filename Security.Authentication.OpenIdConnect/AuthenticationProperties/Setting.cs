
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

  static AuthenticationProperties SetAuthenticationPropertiesTokenLifetime(
    AuthenticationProperties authProperties,
    SecurityToken securityToken
    )
  {
    if (securityToken.ValidFrom > DateTime.MinValue)
      SetAuthenticationPropertiesIssued(authProperties, securityToken.ValidFrom);
    if (securityToken.ValidTo > DateTime.MinValue)
      SetAuthenticationPropertiesExpires(authProperties, securityToken.ValidTo);
    return authProperties;
  }

  static AuthenticationProperties SetChallengeAuthenticationProperties(
    AuthenticationProperties authProperties,
    string requestUrl,
    string redirectUri,
    string state)
  {
    SetAuthenticationPropertiesRedirectUri(authProperties, requestUrl);
    SetAuthenticationPropertiesRedirectUriForCode(authProperties, redirectUri);
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
}
