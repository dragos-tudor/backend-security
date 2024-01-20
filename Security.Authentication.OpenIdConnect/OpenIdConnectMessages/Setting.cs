using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static void SetOpenIdConnectMessageResponseMode(OpenIdConnectMessage oidcMessage, string responseMode) =>
    oidcMessage.ResponseMode = responseMode;

  static void SetOpenIdConnectMessageState(OpenIdConnectMessage oidcMessage, string state) =>
    oidcMessage.State = state;

  static OpenIdConnectMessage SetChallengeOpenIdConnectMessage(
    OpenIdConnectMessage oidcMessage,
    AuthenticationProperties authProperties,
    OpenIdConnectOptions oidcOptions,
    PropertiesDataFormat propertiesDataFormat)
  {
    if (!IsCodeOpenIdConnectResponseType(oidcOptions) ||
        !IsQueryOpenIdConnectResponseMode(oidcOptions))
      SetOpenIdConnectMessageResponseMode(oidcMessage, oidcOptions.ResponseMode);

    if (ShouldUseCodeChallenge(oidcOptions))
      SetAuthorizationParamsCodeChallenge(authProperties, oidcMessage.Parameters);

    SetOpenIdConnectMessageState(oidcMessage, propertiesDataFormat.Protect(authProperties));
    return oidcMessage;
  }
}