
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static OAuthParams SetAuthorizationOAuthParams(
    OAuthParams authParams,
    OAuthOptions authOptions,
    AuthenticationProperties authProps,
    PropertiesDataFormat authPropsProtector,
    string callbackUrl)
  {
    // https://www.ietf.org/rfc/rfc6749.txt [Authorization Request page 25]
    SetOAuthParam(authParams, OAuthParamNames.ClientId, authOptions.ClientId);
    SetOAuthParam(authParams, OAuthParamNames.ResponseType, authOptions.ResponseType);
    SetOAuthParam(authParams, OAuthParamNames.RedirectUri, callbackUrl);
    SetOAuthParam(authParams, OAuthParamNames.Scope, FormatOAuthScopes(authOptions));
    SetOAuthParam(authParams, OAuthParamNames.State, ProtectAuthProps(authProps, authPropsProtector));
    return authParams;
  }
}