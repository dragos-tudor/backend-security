
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static OidcParams SetSignoutChallengeParams(
    OidcParams oidcParams,
    HttpContext context,
    OpenIdConnectOptions oidcOptions,
    string state,
    string idTokenHint)
  {
    SetOAuthParam(oidcParams, OidcParamNames.IdTokenHint, idTokenHint);
    SetOAuthParam(oidcParams, OidcParamNames.PostLogoutRedirectUri, GetAbsoluteUrl(context.Request, oidcOptions.CallbackSignOutPath));
    SetOAuthParam(oidcParams, OidcParamNames.State, state);
    if (!oidcOptions.DisableTelemetry)
      SetTelemetryOidcParams(oidcParams);
    return oidcParams;
  }

  static AuthenticationProperties SetSignoutChallengeAuthProps(
    AuthenticationProperties authProps,
    string redirectUri)
  {
    SetAuthPropsRedirectUri(authProps, redirectUri);
    return authProps;
  }
}