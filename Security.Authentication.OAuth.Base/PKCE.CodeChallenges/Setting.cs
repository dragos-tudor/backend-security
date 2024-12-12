
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string SetOAuthParamsCodeChallenge(
    OAuthParams oauthParams,
    string codeVerifier)
  {
    var codeChallenge = HashCodeVerifier(codeVerifier);
    SetOAuthParam(oauthParams, OAuthParamNames.CodeChallengeMethod, OAuthParamNames.CodeChallengeMethodS256);
    SetOAuthParam(oauthParams, OAuthParamNames.CodeChallenge, codeChallenge);
    return codeChallenge;
  }
}