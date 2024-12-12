
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static OidcParams SetTokenParams(
    OidcParams oidcParams,
    OpenIdConnectOptions oidcOptions,
    string authCode,
    string? codeVerifier,
    string redirectUri)
  {
    SetOAuthParam(oidcParams, OidcParamNames.ClientId, oidcOptions.ClientId);
    SetOAuthParam(oidcParams, OidcParamNames.ClientSecret, oidcOptions.ClientSecret);
    SetOAuthParam(oidcParams, OidcParamNames.Code, authCode);
    SetOAuthParam(oidcParams, OidcParamNames.GrantType, OpenIdConnectGrantTypes.AuthorizationCode);
    SetOAuthParam(oidcParams, OidcParamNames.RedirectUri, redirectUri);

    if (IsNotEmptyString(codeVerifier)) SetOAuthParam(oidcParams, OAuthParamNames.CodeVerifier, codeVerifier!);
    if (!oidcOptions.DisableTelemetry) SetTelemetryOidcParams(oidcParams);
    return oidcParams;
  }
}