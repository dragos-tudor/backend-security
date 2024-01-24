
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static void SetAuthenticationPropertiesCheckSessionIFrame(AuthenticationProperties authProperties, string CheckSessionIframe) =>
    authProperties.Items.Add(OpenIdConnectSessionProperties.CheckSessionIFrame, CheckSessionIframe);

  static void SetAuthenticationPropertiesRedirectUriForCode(AuthenticationProperties authProperties, string redirectUri) =>
    authProperties.Items.Add(OpenIdConnectDefaults.RedirectUriForCodePropertiesKey, redirectUri);

  static string SetAuthenticationPropertiesRedirectUri(AuthenticationProperties authProperties, string redirectUri) =>
    authProperties.RedirectUri = redirectUri;

  static void SetAuthenticationPropertiesSessionState(AuthenticationProperties authProperties, string sessionState) =>
    authProperties.Items.Add(OpenIdConnectSessionProperties.SessionState, sessionState);

  static void SetAuthenticationPropertiesUserState(AuthenticationProperties authProperties, string state) =>
    authProperties.Items.Add(OpenIdConnectDefaults.UserStatePropertiesKey, state);

  static AuthenticationProperties SetAuthenticationPropertiesSession(
    AuthenticationProperties authProperties,
    OpenIdConnectOptions oidcOptions,
    OpenIdConnectMessage oidcMessage)
  {
    if (!IsEmptyString(oidcMessage.SessionState))
      SetAuthenticationPropertiesSessionState(authProperties, oidcMessage.SessionState);

    if (!IsEmptyString(oidcOptions.CheckSessionIframe))
      SetAuthenticationPropertiesCheckSessionIFrame(authProperties, oidcOptions.CheckSessionIframe!);

    return authProperties;
  }

  static AuthenticationProperties SetChallengeAuthenticationProperties(
    AuthenticationProperties authProperties,
    OpenIdConnectMessage oidcMessage,
    string requestUrl)
  {
    if (IsEmptyString(authProperties.RedirectUri))
      SetAuthenticationPropertiesRedirectUri(authProperties, requestUrl);

    if (!IsEmptyString(oidcMessage.State))
      SetAuthenticationPropertiesUserState(authProperties, oidcMessage.State);

    SetAuthenticationPropertiesRedirectUriForCode(authProperties, oidcMessage.RedirectUri);
    return authProperties;
  }
}
