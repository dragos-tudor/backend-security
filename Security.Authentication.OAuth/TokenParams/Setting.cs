
using System.Collections.Generic;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  const string ClientSecret = "client_secret";
  const string AuthorizationCode = "code";
  const string GrantType = "grant_type";
  const string GrantAuthorizationCode = "authorization_code";

  static void SetTokenParam (IDictionary<string, string> tokenParams, string name, string value) =>
    tokenParams.Add(name, value);

  static void SetTokenParamAuthorizationCode (IDictionary<string, string> tokenParams, string authorizationCode) =>
    SetTokenParam(tokenParams, AuthorizationCode, authorizationCode);

  static void SetTokenParamClientId (IDictionary<string, string> tokenParams, string clientId) =>
    SetTokenParam(tokenParams, ClientId, clientId);

  static void SetTokenParamClientSecret (IDictionary<string, string> tokenParams, string clientSecret) =>
    SetTokenParam(tokenParams, ClientSecret, clientSecret);

  static void SetTokenParamCodeVerifier (IDictionary<string, string> tokenParams, string codeVerifier) =>
    SetTokenParam(tokenParams, CodeVerifier, codeVerifier);

  static void SetTokenParamGrantType (IDictionary<string, string> tokenParams, string grantType) =>
    SetTokenParam(tokenParams, GrantType, grantType);

  static void SetTokenParamRedirectUri (IDictionary<string, string> tokenParams, string redirectUri) =>
    SetTokenParam(tokenParams, RedirectUri, redirectUri);

}