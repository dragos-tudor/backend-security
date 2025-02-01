
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static OidcParams SetChallengeOidcParams(
    OidcParams oidcParams,
    OpenIdConnectOptions oidcOptions,
    AuthenticationProperties authProps,
    string callbackUrl,
    string? codeVerifier,
    string? maxAge,
    string state)
  {
    SetOAuthParam(oidcParams, OAuthParamNames.ClientId, oidcOptions.ClientId);
    SetOAuthParam(oidcParams, OAuthParamNames.RedirectUri, callbackUrl);
    SetOAuthParam(oidcParams, OAuthParamNames.ResponseType, oidcOptions.ResponseType);
    SetOAuthParam(oidcParams, OidcParamNames.Resource, oidcOptions.Resource!);
    SetOAuthParam(oidcParams, OidcParamNames.ResponseMode, oidcOptions.ResponseMode);
    SetOAuthParam(oidcParams, OAuthParamNames.Prompt, GetOidcParamPrompt(authProps, oidcOptions));
    SetOAuthParam(oidcParams, OAuthParamNames.Scope, GetOidcParamScope(authProps, oidcOptions));
    SetOAuthParam(oidcParams, OAuthParamNames.State, state);

    if (IsNotEmptyString(codeVerifier)) SetOAuthParamsCodeChallenge(oidcParams, codeVerifier!);
    if (IsNotEmptyString(maxAge)) SetOAuthParam(oidcParams, OidcParamNames.MaxAge, maxAge!);
    if (!oidcOptions.DisableTelemetry) SetTelemetryOidcParams(oidcParams);
    SetOAuthParams(oidcParams, oidcOptions.AdditionalAuthorizationParameters);
    return oidcParams;
  }

  static AuthenticationProperties SetChallengeAuthProps(
    AuthenticationProperties authProps,
    string callbackUrl,
    string correlationId,
    string? codeVerifier,
    string? redirectUri,
    string? state = default)
  {
    SetAuthPropsCorrelationId(authProps, correlationId);
    SetAuthPropsItem(authProps, OidcDefaults.RedirectUriForCodeProperties, callbackUrl);
    if (IsNotEmptyString(codeVerifier)) SetAuthPropsCodeVerifier(authProps, codeVerifier!);
    if (IsNotEmptyString(redirectUri)) SetAuthPropsRedirectUri(authProps, redirectUri!);
    if (IsNotEmptyString(state)) SetAuthPropsItem(authProps, OidcDefaults.UserStateProperties, state!); //TODO: resolve user state
    return authProps;
  }
}
