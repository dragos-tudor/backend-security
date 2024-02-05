
using System.Collections.Generic;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static string SetAuthorizationParamsCodeChallenge(
    IDictionary<string, string> authParams,
    string codeChallenge)
  {
    SetRemoteParamCodeChallenge(authParams, codeChallenge);
    SetRemoteParamCodeChallengeMethod(authParams);
    return codeChallenge;
  }
}