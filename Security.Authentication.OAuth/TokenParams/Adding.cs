
using System.Collections.Generic;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static IDictionary<string, string> AddTokenCodeVerifierParam (IDictionary<string, string> @params, string codeVerifier) {
    @params.Add(CodeVerifier, codeVerifier);
    return @params;
  }

}