using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static OAuthParams SetTokenParams(
    OidcParams oidcParams,
    AuthenticationProperties authProps,
    OpenIdConnectOptions oidcOptions,
    string authCode)
  {
    SetOAuthParam(oidcParams, OidcParamNames.ClientId, oidcOptions.ClientId);
    SetOAuthParam(oidcParams, OidcParamNames.ClientSecret, oidcOptions.ClientSecret);
    SetOAuthParam(oidcParams, OidcParamNames.Code, authCode);
    SetOAuthParam(oidcParams, OidcParamNames.GrantType, OpenIdConnectGrantTypes.AuthorizationCode);
    SetOAuthParam(oidcParams, OidcParamNames.RedirectUri, GetAuthPropsRedirectUriForCode(authProps)!);
    if (!oidcOptions.DisableTelemetry) {
      SetOAuthParam(oidcParams, OidcParamNames.SkuTelemetry, IdentityModelTelemetryUtil.ClientSku);
      SetOAuthParam(oidcParams, OidcParamNames.VersionTelemetry, IdentityModelTelemetryUtil.ClientVer);
    }
    return oidcParams;
  }
}