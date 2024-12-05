
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string UseCodeChallenge(
    OAuthParams oauthParams,
    AuthenticationProperties authProps,
    string codeVerifier)
  {
    var codeChallenge = HashCodeVerifier(codeVerifier);
    SetAuthPropsCodeVerifier(authProps, codeVerifier);
    SetOAuthParam(oauthParams, OAuthParamNames.CodeChallengeMethod, OAuthParamNames.CodeChallengeMethodS256);
    SetOAuthParam(oauthParams, OAuthParamNames.CodeChallenge, codeChallenge);
    return codeChallenge;
  }
}