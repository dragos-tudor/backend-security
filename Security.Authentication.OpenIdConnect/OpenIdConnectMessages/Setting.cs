
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string SetOpenIdConnectMessageAuthorizationCode(OpenIdConnectMessage oidcMessage, string authCode) =>
    oidcMessage.Code = authCode;

  static string SetOpenIdConnectMessageClientId(OpenIdConnectMessage oidcMessage, string clientId) =>
    oidcMessage.ClientId = clientId;

  static string SetOpenIdConnectMessageClientSecret(OpenIdConnectMessage oidcMessage, string clientSecret) =>
    oidcMessage.ClientSecret = clientSecret;

  static string SetOpenIdConnectMessageCodeVerifier(OpenIdConnectMessage oidcMessage, string codeVerifier)
  { oidcMessage.Parameters.Add(OAuthConstants.CodeVerifierKey, codeVerifier); return codeVerifier; }

  static bool SetOpenIdConnectMessageEnableTelemetry(OpenIdConnectMessage oidcMessage, bool enableTelemetry) =>
    oidcMessage.EnableTelemetryParameters = enableTelemetry;

  static string SetOpenIdConnectMessageGrantType(OpenIdConnectMessage oidcMessage, string grantType) =>
    oidcMessage.GrantType = grantType;

  static string? SetOpenIdConnectMessageIdTokenHint(OpenIdConnectMessage oidcMessage, string? idTokenHint) =>
    oidcMessage.IdTokenHint = idTokenHint;

  static string SetOpenIdConnectMessageIssuerAddress(OpenIdConnectMessage oidcMessage, string issuerAddress) =>
    oidcMessage.IssuerAddress = issuerAddress;

  static string? SetOpenIdConnectMessageMaxAge(OpenIdConnectMessage oidcMessage, string? maxAge) =>
    oidcMessage.MaxAge = maxAge;

  static string SetOpenIdConnectMessageNonce(OpenIdConnectMessage oidcMessage, string nonce) =>
    oidcMessage.Nonce = nonce;

  static string? SetOpenIdConnectMessagePostLogoutRedirectUri(OpenIdConnectMessage oidcMessage, string? redirectUri) =>
    oidcMessage.PostLogoutRedirectUri = redirectUri;

  static string? SetOpenIdConnectMessagePrompt(OpenIdConnectMessage oidcMessage, string? prompt) =>
    oidcMessage.Prompt = prompt;

  static string? SetOpenIdConnectMessageResource(OpenIdConnectMessage oidcMessage, string? resource) =>
    oidcMessage.Resource = resource;

  static string SetOpenIdConnectMessageRedirectUri(OpenIdConnectMessage oidcMessage, string redirectUri) =>
    oidcMessage.RedirectUri = redirectUri;

  static string SetOpenIdConnectMessageResponseMode(OpenIdConnectMessage oidcMessage, string responseMode) =>
    oidcMessage.ResponseMode = responseMode;

  static string SetOpenIdConnectMessageResponseType(OpenIdConnectMessage oidcMessage, string responseType) =>
    oidcMessage.ResponseType = responseType;

  static string? SetOpenIdConnectMessageScope(OpenIdConnectMessage oidcMessage, string? scope) =>
    oidcMessage.Scope = scope;

  static string SetOpenIdConnectMessageState(OpenIdConnectMessage oidcMessage, string state) =>
    oidcMessage.State = state;

  static OpenIdConnectMessage SetAuthorizationOpenIdConnectMessage(
    OpenIdConnectMessage oidcMessage,
    HttpContext context,
    AuthenticationProperties authProperties,
    OpenIdConnectOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    string state)
  {
    SetOpenIdConnectMessageClientId(oidcMessage, oidcOptions.ClientId);
    SetOpenIdConnectMessageEnableTelemetry(oidcMessage, !oidcOptions.DisableTelemetry);
    SetOpenIdConnectMessageMaxAge(oidcMessage, GetOpenIdConnectMessageMaxAge(authProperties, oidcOptions));
    SetOpenIdConnectMessageIssuerAddress(oidcMessage, oidcConfiguration?.AuthorizationEndpoint ?? string.Empty);
    SetOpenIdConnectMessageRedirectUri(oidcMessage, GetOpenIdConnectMessageRedirectUri(context, oidcOptions));
    SetOpenIdConnectMessageResource(oidcMessage, oidcOptions.Resource);
    SetOpenIdConnectMessageResponseType(oidcMessage, oidcOptions.ResponseType);
    SetOpenIdConnectMessagePrompt(oidcMessage, GetOpenIdConnectMessagePrompt(authProperties, oidcOptions));
    SetOpenIdConnectMessageScope(oidcMessage, GetOpenIdConnectMessageScope(authProperties, oidcOptions));
    SetOpenIdConnectMessageState(oidcMessage, state);
    if (IsResponseModeOpenIdConnectSettable(oidcOptions))
      SetOpenIdConnectMessageResponseMode(oidcMessage, oidcOptions.ResponseMode);
    return oidcMessage;
  }

  static OpenIdConnectMessage SetPostAuthorizationOpenIdConnectMessage(
    OpenIdConnectMessage oidcMessage,
    AuthenticationProperties authProperties)
  {
    SetOpenIdConnectMessageState(oidcMessage, GetAuthenticationPropertiesUserState(authProperties)!);
    return oidcMessage;
  }

  static async Task<OpenIdConnectMessage> SetSignoutChallengeOpenIdConnectMessage(
    OpenIdConnectMessage oidcMessage,
    HttpContext context,
    OpenIdConnectOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    string state)
  {
    SetOpenIdConnectMessageEnableTelemetry(oidcMessage, !oidcOptions.DisableTelemetry);
    SetOpenIdConnectMessageIssuerAddress(oidcMessage, oidcConfiguration.EndSessionEndpoint ?? string.Empty);
    SetOpenIdConnectMessageIdTokenHint(oidcMessage, await GetOpenIdConnectMessageIdTokenHint(context, oidcOptions));
    SetOpenIdConnectMessagePostLogoutRedirectUri(oidcMessage, BuildAbsoluteUrl(context.Request, oidcOptions.SignOutCallbackPath));
    SetOpenIdConnectMessageState(oidcMessage, state);
    return oidcMessage;
  }
}