
namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static string? GetAccessToken (TokenResult tokenResult) =>
    tokenResult.TokenInfo?.AccessToken;

}