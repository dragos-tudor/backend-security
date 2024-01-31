
using System.Collections.Generic;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  const string ClientSecret = "client_secret";
  const string AuthorizationCode = "code";
  const string GrantType = "grant_type";
  const string GrantAuthorizationCode = "authorization_code";

  static void SetTokenParamAuthorizationCode (IDictionary<string, string> tokenParams, string authorizationCode) =>
    SetRemoteParam(tokenParams, AuthorizationCode, authorizationCode);

  static void SetTokenParamClientId (IDictionary<string, string> tokenParams, string clientId) =>
    SetRemoteParam(tokenParams, ClientId, clientId);

  static void SetTokenParamClientSecret (IDictionary<string, string> tokenParams, string clientSecret) =>
    SetRemoteParam(tokenParams, ClientSecret, clientSecret);

  static void SetTokenParamCodeVerifier (IDictionary<string, string> tokenParams, string codeVerifier) =>
    SetRemoteParamCodeVerifier(tokenParams, codeVerifier);

  static void SetTokenParamGrantType (IDictionary<string, string> tokenParams, string grantType) =>
    SetRemoteParam(tokenParams, GrantType, grantType);

  static void SetTokenParamRedirectUri (IDictionary<string, string> tokenParams, string redirectUri) =>
    SetRemoteParam(tokenParams, RedirectUri, redirectUri);

}