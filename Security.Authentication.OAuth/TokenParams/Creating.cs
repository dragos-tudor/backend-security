
using System.Collections.Generic;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  const string ClientSecret = "client_secret";
  const string AuthorizationCode = "code";
  const string GrantType = "grant_type";
  const string GrantAuthorizationCode = "authorization_code";

  static Dictionary<string, string> CreateTokenParams (OAuthOptions authOptions, string code, string redirectUri) => new () {
    { ClientId, authOptions.ClientId },
    { ClientSecret, authOptions.ClientSecret },
    { AuthorizationCode, code },
    { GrantType, GrantAuthorizationCode },
    { RedirectUri, redirectUri }
  };

}