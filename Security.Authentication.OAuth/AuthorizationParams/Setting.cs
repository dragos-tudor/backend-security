
using System.Collections.Generic;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  const string ClientId = "client_id";
  const string ResponseType = "response_type";
  const string RedirectUri = "redirect_uri";
  const string Scope = "scope";
  const string State = "state";

  static void SetAuthorizationParamClientId (IDictionary<string, string> authParams, string clientId) =>
    SetRemoteParam(authParams, ClientId, clientId);

  static void SetAuthorizationParamResponseType (IDictionary<string, string> authParams, string responseType) =>
    SetRemoteParam(authParams, ResponseType, responseType);

  static void SetAuthorizationParamRedirectUri (IDictionary<string, string> authParams, string redirectUri) =>
    SetRemoteParam(authParams, RedirectUri, redirectUri);

  static void SetAuthorizationParamScope (IDictionary<string, string> authParams, string scope) =>
    SetRemoteParam(authParams, Scope, scope);

  static void SetAuthorizationParamState (IDictionary<string, string> authParams, string state) =>
    SetRemoteParam(authParams, State, state);

}