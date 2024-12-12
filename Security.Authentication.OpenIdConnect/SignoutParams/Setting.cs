
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static OidcParams SetChallengeSignoutParams(
    OidcParams oidcParams,
    OpenIdConnectOptions oidcOptions,
    string idTokenHint,
    string redirectUri,
    string state)
  {
    SetOAuthParam(oidcParams, OidcParamNames.IdTokenHint, idTokenHint);
    SetOAuthParam(oidcParams, OidcParamNames.PostLogoutRedirectUri, redirectUri);
    SetOAuthParam(oidcParams, OidcParamNames.State, state);
    if (!oidcOptions.DisableTelemetry) SetTelemetryOidcParams(oidcParams);
    return oidcParams;
  }

  static AuthenticationProperties SetChallengeSignoutAuthProps(
    AuthenticationProperties authProps,
    string redirectUri)
  {
    SetAuthPropsRedirectUri(authProps, redirectUri);
    return authProps;
  }
}