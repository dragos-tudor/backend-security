using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static string UseCodeChallenge(
    AuthenticationProperties authProperties,
    IDictionary<string, string> authParams,
    string codeVerifier)
  {
    var codeChallenge = HashCodeVerifier(codeVerifier);
    SetAuthenticationPropertiesCodeVerifier(authProperties, codeVerifier);
    SetAuthorizationParamsCodeChallenge(authParams, codeChallenge);
    return codeChallenge;
  }
}