
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string SetAuthenticationPropertiesRedirectUri (AuthenticationProperties authProperties, string redirectUri) =>
    authProperties.RedirectUri = redirectUri;

  static void SetAuthenticationPropertiesUserState (AuthenticationProperties authProperties, string state) =>
    authProperties.Items.Add(OpenIdConnectDefaults.UserStatePropertiesKey, state);

  static void SetAuthenticationPropertiesRedirectUriForCode (AuthenticationProperties authProperties, string redirectUri) =>
    authProperties.Items.Add(OpenIdConnectDefaults.RedirectUriForCodePropertiesKey, redirectUri);

  static AuthenticationProperties SetChallengeAuthenticationProperties(
    AuthenticationProperties authProperties,
    OpenIdConnectOptions oidcOptions,
    OpenIdConnectMessage oidcMessage,
    string requestUrl)
  {
    if (IsEmptyString(authProperties.RedirectUri))
      SetAuthenticationPropertiesRedirectUri(authProperties, requestUrl);

    if (!IsEmptyString(oidcMessage.State))
      SetAuthenticationPropertiesUserState(authProperties, oidcMessage.State);

    if (ShouldUseCodeChallenge(oidcOptions))
      SetAuthenticationPropertiesCodeVerifier(authProperties, GenerateCodeVerifier());

    SetAuthenticationPropertiesRedirectUriForCode(authProperties, oidcMessage.RedirectUri);
    return authProperties;
  }
}
