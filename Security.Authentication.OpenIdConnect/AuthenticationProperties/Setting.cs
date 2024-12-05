
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static AuthenticationProperties SetAuthPropsTokens(
    AuthenticationProperties authProps,
    OidcTokens tokens)
  {
    if (IsNotEmptyString(tokens.IdToken)) SetAuthPropsItem(authProps, OidcParamNames.IdToken, tokens.IdToken);
    return OAuthBaseFuncs.SetAuthPropsTokens(authProps, (OAuthTokens)tokens);
  }

  static AuthenticationProperties SetAuthPropsTokenLifetime(
    AuthenticationProperties authProps,
    SecurityToken securityToken)
  {
    if (HasSecurityTokenValidFrom(securityToken)) SetAuthPropsIssued(authProps, securityToken.ValidFrom);
    if (HasSecurityTokenValidTo(securityToken)) SetAuthPropsExpires(authProps, securityToken.ValidTo);
    return authProps;
  }

  static AuthenticationProperties SetAuthPropsSession(
    AuthenticationProperties authProps,
    OpenIdConnectOptions oidcOptions,
    OidcData oidcData)
  {
    if (GetOidcDataSessionState(oidcData) is string sessionState) SetAuthPropsItem(authProps, OpenIdConnectSessionProperties.SessionState, sessionState);
    if (IsNotEmptyString(oidcOptions.CheckSessionIframe)) SetAuthPropsItem(authProps, OpenIdConnectSessionProperties.CheckSessionIFrame, oidcOptions.CheckSessionIframe);
    return authProps;
  }
}
