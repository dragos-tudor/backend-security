using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static AuthenticationProperties SetChallengeAuthenticationProperties(
    AuthenticationProperties authProperties,
    string callbackUrl,
    string correlationId,
    string redirectUri)
  {
    SetAuthenticationPropertiesCorrelationId(authProperties, correlationId);
    SetAuthenticationPropertiesCallbackUri(authProperties, callbackUrl);
    SetAuthenticationPropertiesRedirectUri(authProperties, redirectUri);
    return authProperties;
  }

}