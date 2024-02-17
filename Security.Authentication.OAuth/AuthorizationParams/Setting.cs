
using System.Collections.Generic;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  const string ClientId = "client_id";
  const string ResponseType = "response_type";
  const string RedirectUri = "redirect_uri";
  const string Scope = "scope";
  const string State = "state";

  static string SetAuthorizationParamClientId (IDictionary<string, string> authParams, string clientId) =>
    SetRemoteParam(authParams, ClientId, clientId);

  static string SetAuthorizationParamResponseType (IDictionary<string, string> authParams, string responseType) =>
    SetRemoteParam(authParams, ResponseType, responseType);

  static string SetAuthorizationParamRedirectUri (IDictionary<string, string> authParams, string redirectUri) =>
    SetRemoteParam(authParams, RedirectUri, redirectUri);

  static string SetAuthorizationParamScope (IDictionary<string, string> authParams, string scope) =>
    SetRemoteParam(authParams, Scope, scope);

  static string SetAuthorizationParamState (IDictionary<string, string> authParams, string state) =>
    SetRemoteParam(authParams, State, state);

  static IDictionary<string, string> SetAuthorizationParams<TOptions>(
    IDictionary<string, string> authParams,
    TOptions authOptions,
    string callbackUrl,
    string state,
    string responseType = "code") where TOptions : OAuthOptions
  {
    // https://www.ietf.org/rfc/rfc6749.txt [Authorization Request page 25]
    SetAuthorizationParamClientId(authParams, authOptions.ClientId);
    SetAuthorizationParamResponseType(authParams, responseType);
    SetAuthorizationParamRedirectUri(authParams, callbackUrl);
    SetAuthorizationParamScope(authParams, FormatOAuthScopes(authOptions));
    SetAuthorizationParamState(authParams, state);
    return authParams;
  }

}