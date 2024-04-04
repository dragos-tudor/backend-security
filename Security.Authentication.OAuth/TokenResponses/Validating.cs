
namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  internal const string AccessTokenNotFound = "Failed to retrieve access token";

  static TokenResult ValidateTokenResult (TokenResult tokenResult) {
    if (!ExistsAccessToken(tokenResult)) return CreateFailureTokenResult(AccessTokenNotFound);
    return tokenResult;
  }

}