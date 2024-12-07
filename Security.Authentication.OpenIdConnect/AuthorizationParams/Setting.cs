
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static OidcParams SetAuthorizationOidcParams(
    OidcParams oidcParams,
    AuthenticationProperties authProps,
    OpenIdConnectOptions oidcOptions,
    string state,
    string callbackUri)
  {
    SetOAuthParam(oidcParams, OAuthParamNames.ClientId, oidcOptions.ClientId);
    SetOAuthParam(oidcParams, OAuthParamNames.RedirectUri, callbackUri);

    SetOAuthParam(oidcParams, OAuthParamNames.ResponseType, oidcOptions.ResponseType);
    SetOAuthParam(oidcParams, OidcParamNames.Resource, oidcOptions.Resource!);
    SetOAuthParam(oidcParams, OidcParamNames.ResponseMode, oidcOptions.ResponseMode);

    SetOAuthParam(oidcParams, OAuthParamNames.Prompt, GetOidcParamPrompt(authProps, oidcOptions));
    SetOAuthParam(oidcParams, OAuthParamNames.Scope, GetOidcParamScope(authProps, oidcOptions));
    SetOAuthParam(oidcParams, OAuthParamNames.State, state);

    if (GetOidcParamMaxAge(authProps, oidcOptions) is string maxAge) SetOAuthParam(oidcParams, OidcParamNames.MaxAge, maxAge);
    if (!oidcOptions.DisableTelemetry) SetTelemetryOidcParams(oidcParams);

    return oidcParams;
  }

  static void SetTelemetryOidcParams(OidcParams oidcParams)
  {
    SetOAuthParam(oidcParams, OidcParamNames.SkuTelemetry, IdentityModelTelemetryUtil.ClientSku);
    SetOAuthParam(oidcParams, OidcParamNames.VersionTelemetry, IdentityModelTelemetryUtil.ClientVer);
  }

  static AuthenticationProperties SetAuthorizationAuthProps(
    AuthenticationProperties authProps,
    string redirectUri,
    string callbackUri,
    string? state = default)
  {
    SetAuthPropsRedirectUri(authProps, redirectUri);
    SetAuthPropsItem(authProps, OidcDefaults.RedirectUriForCodeProperties, callbackUri);
    SetAuthPropsItem(authProps, OidcDefaults.UserStateProperties, state); //TODO: resolve user state
    return authProps;
  }
}
