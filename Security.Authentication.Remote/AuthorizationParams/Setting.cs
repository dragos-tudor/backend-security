
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static IDictionary<string, string> SetAuthorizationParamsCodeChallenge(
    IDictionary<string, string> authParams,
    AuthenticationProperties authProperties)
  {
    var codeVerifier = GetAuthenticationPropertiesCodeVerifier(authProperties)!;
    var codeChallenge = HashCodeVerifier(codeVerifier);
    SetRemoteParamCodeChallenge(authParams, codeChallenge);
    SetRemoteParamCodeChallengeMethod(authParams);
    return authParams;
  }
}