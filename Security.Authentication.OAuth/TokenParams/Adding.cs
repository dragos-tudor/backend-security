
using System.Collections.Generic;

namespace Security.Authentication.OAuth;

partial class Funcs {

  static IDictionary<string, string> AddTokenCodeVerifierParam (IDictionary<string, string> @params, string codeVerifier) {
    @params.Add(CodeVerifier, codeVerifier);
    return @params;
  }

}