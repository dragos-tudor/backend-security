
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string UseCodeChallenge(
    OAuthParams authParams,
    AuthenticationProperties authProps,
    string codeVerifier)
  {
    var codeChallenge = HashCodeVerifier(codeVerifier);
    SetAuthPropsCodeVerifier(authProps, codeVerifier);
    SetOAuthParam(authParams, OAuthParamNames.CodeChallengeMethod, OAuthParamNames.CodeChallengeMethodS256);
    SetOAuthParam(authParams, OAuthParamNames.CodeChallenge, codeChallenge);
    return codeChallenge;
  }
}