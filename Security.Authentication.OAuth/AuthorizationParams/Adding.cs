
using System.Collections.Generic;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static IDictionary<string, string> AddAuthorizationCodeChallengeParams (IDictionary<string, string> @params, string codeChallenge) {
    @params.Add(CodeChallenge, codeChallenge);
    @params.Add(CodeChallengeMethod, CodeChallengeMethodS256);
    return @params;
  }

}