using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static IDictionary<string, string> BuildTokenParams(
    AuthenticationProperties authProperties,
    OpenIdConnectOptions oidcOptions,
    string authCode)
  {
    var tokenMessage = CreateOpenIdConnectMessage();

    SetOpenIdConnectMessageClientId(tokenMessage, oidcOptions.ClientId);
    SetOpenIdConnectMessageClientSecret(tokenMessage, oidcOptions.ClientSecret);
    SetOpenIdConnectMessageAuthorizationCode(tokenMessage, authCode);
    SetOpenIdConnectMessageGrantType(tokenMessage, OpenIdConnectGrantTypes.AuthorizationCode);
    SetOpenIdConnectMessageEnableTelemetry(tokenMessage, !oidcOptions.DisableTelemetry);
    SetOpenIdConnectMessageRedirectUri(tokenMessage, GetAuthenticationPropertiesRedirectUriForCode(authProperties)!);
    if(GetAuthenticationPropertiesCodeVerifier(authProperties) is string codeVerifier)
      SetOpenIdConnectMessageCodeVerifier(tokenMessage, codeVerifier);

    return tokenMessage.Parameters;
  }
}