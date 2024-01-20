
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  const string ClientId = "client_id";
  const string ResponseType = "response_type";
  const string RedirectUri = "redirect_uri";
  const string Scope = "scope";
  const string State = "state";

  static void SetAuthorizationParam (IDictionary<string, string> authParams, string paramName, string paramValue) =>
    authParams.Add(paramName, paramValue);

  static void SetAuthorizationParamCodeChallenge (IDictionary<string, string> authParams, string codeChallenge) =>
    SetAuthorizationParam(authParams, CodeChallenge, codeChallenge);

  static void SetAuthorizationParamCodeChallengeMethod (IDictionary<string, string> authParams) =>
    SetAuthorizationParam(authParams, CodeChallengeMethod, CodeChallengeMethodS256);

  static void SetAuthorizationParamClientId (IDictionary<string, string> authParams, string clientId) =>
    SetAuthorizationParam(authParams, ClientId, clientId);

  static void SetAuthorizationParamResponseType (IDictionary<string, string> authParams, string responseType) =>
    SetAuthorizationParam(authParams, ResponseType, responseType);

  static void SetAuthorizationParamRedirectUri (IDictionary<string, string> authParams, string redirectUri) =>
    SetAuthorizationParam(authParams, RedirectUri, redirectUri);

  static void SetAuthorizationParamScope (IDictionary<string, string> authParams, string scope) =>
    SetAuthorizationParam(authParams, Scope, scope);

  static void SetAuthorizationParamState (IDictionary<string, string> authParams, string state) =>
    SetAuthorizationParam(authParams, State, state);

  public static IDictionary<string, string> SetAuthorizationParamsCodeChallenge(
    AuthenticationProperties authProperties,
    IDictionary<string, string> authParams)
  {
    var codeVerifier = GetAuthenticationPropertiesCodeVerifier(authProperties)!;
    var codeChallenge = HashCodeVerifier(codeVerifier);
    SetAuthorizationParamCodeChallenge(authParams, codeChallenge);
    SetAuthorizationParamCodeChallengeMethod(authParams);
    return authParams;
  }
}