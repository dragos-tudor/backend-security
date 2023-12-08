
namespace Security.Authentication.OAuth;

partial class Funcs {

  internal const string AccessTokenNotFound = "Failed to retrieve access token";

  static TokenResult ValidateTokenResult (TokenResult tokenResult) {
    if (!ExistsAccessToken(tokenResult)) return CreateFailureTokenResult(AccessTokenNotFound);
    return tokenResult;
  }

}