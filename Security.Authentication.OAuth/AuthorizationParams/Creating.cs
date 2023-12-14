
using System.Collections.Generic;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  const string ClientId = "client_id";
  const string ResponseType = "response_type";
  const string RedirectUri = "redirect_uri";
  const string Scope = "scope";
  const string State = "state";

  // https://www.ietf.org/rfc/rfc6749.txt [Authorization Request page 25]
  static IDictionary<string, string> CreateAuthorizationParams (
    string clientId,
    string scope,
    string state,
    string redirectUri) =>
      new Dictionary<string, string>() {
        { ClientId, clientId },
        { ResponseType, "code" },
        { RedirectUri, redirectUri },
        { Scope, scope },
        { State, state}
      };

}