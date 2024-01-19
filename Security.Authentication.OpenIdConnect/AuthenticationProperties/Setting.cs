
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string SetAuthenticationPropertiesRedirectUri (AuthenticationProperties authProperties, string redirectUri) =>
      authProperties.RedirectUri = redirectUri;

  static void SetAuthenticationPropertiesUserState (AuthenticationProperties authProperties, string state) =>
    authProperties.Items.Add(OpenIdConnectDefaults.UserStatePropertiesKey, state);

  static void SetAuthenticationPropertiesRedirectUriForCode (AuthenticationProperties authProperties, string redirectUri) =>
    authProperties.Items.Add(OpenIdConnectDefaults.RedirectUriForCodePropertiesKey, redirectUri);
}
