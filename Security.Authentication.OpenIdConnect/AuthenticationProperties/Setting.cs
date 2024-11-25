
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static AuthenticationProperties SetAuthPropsTokens(
    AuthenticationProperties authProps,
    string? idToken,
    TokenInfo tokenInfo)
  {
    if (IsNotEmptyString(idToken)) SetAuthPropsItem(authProps, OidcParamNames.IdToken, idToken);
    if (IsNotEmptyString(tokenInfo.TokenType)) SetAuthPropsItem(authProps, OidcParamNames.TokenType, tokenInfo.TokenType);
    if (IsNotEmptyString(tokenInfo.AccessToken)) SetAuthPropsItem(authProps, OidcParamNames.AccessToken, tokenInfo.AccessToken);
    if (IsNotEmptyString(tokenInfo.RefreshToken)) SetAuthPropsItem(authProps, OidcParamNames.RefreshToken, tokenInfo.RefreshToken);
    return authProps;
  }

  static AuthenticationProperties SetAuthPropsTokenLifetime(
    AuthenticationProperties authProps,
    SecurityToken securityToken)
  {
    if (HasSecurityTokenValidFrom(securityToken)) SetAuthPropsIssued(authProps, securityToken.ValidFrom);
    if (HasSecurityTokenValidTo(securityToken)) SetAuthPropsExpires(authProps, securityToken.ValidTo);
    return authProps;
  }

  static AuthenticationProperties SetPostAuthorizationAuthProps(
    AuthenticationProperties authProps,
    OpenIdConnectMessage oidcMessage,
    OpenIdConnectOptions oidcOptions)
  {
    if (IsNotEmptyString(oidcMessage.SessionState)) SetAuthPropsItem(authProps, OpenIdConnectSessionProperties.SessionState, oidcMessage.SessionState);
    if (IsNotEmptyString(oidcOptions.CheckSessionIframe)) SetAuthPropsItem(authProps, OpenIdConnectSessionProperties.CheckSessionIFrame, oidcOptions.CheckSessionIframe);
    return authProps;
  }

  static AuthenticationProperties SetSignoutChallengeAuthProps(
    AuthenticationProperties authProps,
    string redirectUri,
    string alternateRedirectUri)
  {
    SetAuthPropsRedirectUri(authProps, redirectUri);
    if (IsEmptyUri(GetAuthPropsRedirectUri(authProps)))
      SetAuthPropsRedirectUri(authProps, alternateRedirectUri);
    return authProps;
  }
}
